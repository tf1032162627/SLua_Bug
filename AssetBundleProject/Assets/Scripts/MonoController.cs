using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono的管理者
/// 声明周期函数
/// 事件委托
/// 协程
/// </summary>
public class MonoController : MonoBehaviour
{
    //用来存储想要在update上使用的函数委托
    public event UnityAction updateEvent;
    public event UnityAction fixUpdateEvent;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update() {
        //如果这个事件不为空，则每次帧更新的时候都运行这个事件里的所有委托
        if (updateEvent != null)
        {
            updateEvent();
        }
    }

    private void FixedUpdate()
    {
        if (fixUpdateEvent != null) {
            fixUpdateEvent();
        }
    }

    /// <summary>
    /// 给外部添加帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>

    public void AddUpdateListener(UnityAction fun) {
        updateEvent += fun;
    }
    public void AddFixUpdateListener(UnityAction fun)
    {
        fixUpdateEvent += fun;
    }

    /// <summary>
    /// 提供给外部 用于移除帧更新事件函数
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateListener(UnityAction fun) {
        updateEvent -= fun;
    }

    public void RemoveFixUpdateListener(UnityAction fun)
    {
        fixUpdateEvent -= fun;
    }
}
