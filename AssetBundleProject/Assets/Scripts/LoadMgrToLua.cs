using SLua;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CustomLuaClass]
public class LoadMgrToLua 
{
    public MonoManager GetMonoMgr() {
        return MonoManager.GetInstance();
    
    }
    public ABMgr GetABMgr()
    {
        return ABMgr.GetInstance();
    }

    public PoolMgr GetPoolMgr() {
        return PoolMgr.GetInstance();
    }

    public EventCenter GetEventCenter() {
        return EventCenter.GetInstance();
    }
}
