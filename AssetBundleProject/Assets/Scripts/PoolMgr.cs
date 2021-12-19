using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 缓存池模块
/// </summary>
/// 



public class PoolData {
    //抽屉中对象挂载的父节点
    public GameObject fatherObj;

    //对象的容器
    public List<GameObject> poolList;

    public PoolData(GameObject obj,GameObject poolObj) {        //obj：建立完抽屉后需要存储到里面的抽屉    poolObj：父节点对象，也就相当于是上级文件夹
        
        //给我们的抽屉创建一个父对象，并且把他作为我们pool衣柜对象的物体，就相当于创建了一个文件夹
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;     //设置该父对象的父对象，就像相当于设置这个文件夹的上级文件夹

        poolList = new List<GameObject>() { obj };      //创建一个list，并且将传递过来的obj存储到这里面，因为创建抽屉的时候是当时没有发现该名称的抽屉，所以一定会有需要存储到该抽屉的对象
    }

    /// <summary>
    /// 往抽屉里面压东西
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj) {
        obj.SetActive(false);
        //存进来
        poolList.Add(obj);
        //设置父节点
        obj.transform.parent = fatherObj.transform;
    }

    public GameObject GetObj() {
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    
    }

    
}


public class PoolMgr : BaseManager<PoolMgr>
{
    public Dictionary<string,PoolData> poolDic = new Dictionary<string, PoolData>();    //字典，因为之前的List封装成了PoolData(抽屉)类，所以字典对应的value就为它了

    private GameObject poolObj;             //根节点，所有缓存对象的最终父级
    /// <summary>
    /// 拿东西
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public void GetObj(string name,UnityAction<GameObject> callBack) {

        //有抽屉，抽屉有东西
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            callBack(poolDic[name].GetObj());
            
        }
        else {
            ResMgr.GetInstance().LoadAsync<GameObject>(name,(o)=> { 
                o.name = name;
                callBack(o);
            });
            //obj = GameObject.Instantiate(Resources.Load<GameObject>(name));     //加载资源并实例化对象
            //obj.name = name;        //将资源的路径名作为实例的名字，方便管理和查看
        }
    }

    /// <summary>
    /// 换暂时不用的东西给缓存池
    /// </summary>
    public void PushObj(string name, GameObject obj) {
        if (poolObj == null)            //如果根节点为空
        {
            poolObj = new GameObject("Pool");           //就建立一个根节点
        }

        //里面有抽屉
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);         
        }
        //里面没有抽屉
        else {

            poolDic.Add(name, new PoolData(obj,poolObj));       //建立该抽屉类PoolData并且把需要存储的对象以及父节点的对象传递进去当做参数
            
        }


    }
    /// <summary>
    /// 清空缓存池，用在切换场景之类的
    /// </summary>
    public void Clear() {
        poolDic.Clear();
        poolObj = null;
    
    }

}
