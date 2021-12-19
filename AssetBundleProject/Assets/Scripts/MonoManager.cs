using SLua;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 给外部提供帧更新事件的方法
/// 可以给外部提供添加协程的方法
/// </summary>

[CustomLuaClass]
public class MonoManager : BaseManager<MonoManager>
{
    //controller继承了MonoBehaviour，所以可以使用这些只能继承MonoBehaviour才能使用的函数等
    public MonoController controller;

    public MonoManager() {
        //保证了MonoController的唯一性
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
        
    }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    public void AddUpdateListener(UnityAction fun)
    {
        //Manager封装了一层controller，实质使用的是controller的相应函数
        controller.AddUpdateListener(fun);
    }

    public void AddFixUpdateListener(UnityAction fun)
    {
        //Manager封装了一层controller，实质使用的是controller的相应函数
        controller.AddFixUpdateListener(fun);
    }

    public void RemoveUpdateListener(UnityAction fun)
    {
        controller.RemoveUpdateListener(fun);
    }
    public void RemoveFixUpdateListener(UnityAction fun)
    {
        controller.RemoveFixUpdateListener(fun);
    }

    public Coroutine StartCoroutine(IEnumerator routine) {
        return controller.StartCoroutine(routine);    
    }
    public void StopCoroutine(IEnumerator routine) {
        controller.StopCoroutine(routine);
    }
}
