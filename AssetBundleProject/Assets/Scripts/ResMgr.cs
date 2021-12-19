using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResMgr : BaseManager<ResMgr>
{
    //同步加载资源
    public T Load<T>(string name) where T : Object{
        T res = Resources.Load<T>(name);

        //如果对象是一个gameobject类型的，就把他实例化后返回外部，直接使用即可。
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
        
    }



    //异步加载资源
    public void LoadAsync<T>(string name,UnityAction<T> callback) where T : Object {
        MonoManager.GetInstance().StartCoroutine(ReallyLoadAsync(name,callback));   //使用MonoManager来开启线程，因为这个类没有继承MonoBehaviour
    
    }


    //真正的协同程序函数 用于开启异步加载对应的资源
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">要加载的路径(Resources文件夹内)及名字</param>
    /// <param name="callback">一个委托，可以等到加载之后使用该资源做事情</param>
    /// <returns></returns>
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object {
        ResourceRequest r = Resources.LoadAsync<T>(name);       //异步加载资源

        yield return r;
        //等到资源加载完成之后再进行下一步
        if (r.asset is GameObject)
            callback(GameObject.Instantiate(r.asset) as T);
        else
            callback(r.asset as T);
    }


}
