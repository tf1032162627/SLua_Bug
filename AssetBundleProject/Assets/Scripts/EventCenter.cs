using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 事件中心 单例模式 观察者模式
/// </summary>

public interface IEventInfo { 

    
}

public class EventInfo<T> : IEventInfo{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action) {
        actions += action;
    }
}
public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventCenter : BaseManager<EventCenter>
{
    //key-事件的名字(比如怪物死亡等)    value-监听这个事件对应的委托函数们
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();


    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">准备用来处理事件的委托函数</param>
    public void AddEventListener<T>(string name, UnityAction<T> action) {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(name))
        {
            if((eventDic[name] as EventInfo<T>).actions != null)
                (eventDic[name] as EventInfo<T>).actions += action;
        }
        //没有的情况
        else {
            eventDic.Add(name,new EventInfo<T>(action));
        }
    
    }

    public void AddEventListener(string name, UnityAction action)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
                (eventDic[name] as EventInfo).actions += action;
        }
        //没有的情况
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }

    }

    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">哪一个名字的事件触发了</param>
    public void EventTrigger<T>(string name,T info) {

        if (eventDic.ContainsKey(name))
        {
            //eventDic[name]();
            if((eventDic[name] as EventInfo<T>).actions != null)
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);

        }
        else { 
            //没有人关心这个事件，没啥可触发的
            

        }

    }

    public void EventTrigger(string name)
    {

        if (eventDic.ContainsKey(name))
        {
            //eventDic[name]();
            if ((eventDic[name] as EventInfo).actions != null)
                (eventDic[name] as EventInfo).actions.Invoke();

        }
        else
        {
            //没有人关心这个事件，没啥可触发的


        }

    }

    /// <summary>
    /// 移除对应的事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">对应之前添加的委托函数</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action) {

        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions -= action;

    }

    public void RemoveEventListener(string name, UnityAction action)
    {

        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions -= action;

    }

    /// <summary>
    /// 清空事件中心
    /// </summary>
    public void Clear() {
        eventDic.Clear();
    }
}
