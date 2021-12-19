using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;
using System.IO;

[CustomLuaClass]
public class LuaAddComponent : MonoBehaviour
{


    public string luaName;

    LuaSvr luaSvr;
    LuaTable self;
    LuaFunction update;
    LuaFunction fixUpdate;

    [CustomLuaClass]
    public delegate void UpdateDelegate(object self);
    public delegate void FixUpdateDelegate(object self);

    UpdateDelegate ud;
    FixUpdateDelegate fud;
    
    //LuaFunction function;
    void Start()
    {
        LoadResLua();
        luaSvr = LuaMgr.GetInstance().Init();
        
        LuaCompleteFunc();
        
    }

    void LuaCompleteFunc() {
        //string path = Application.dataPath + "/Lua/" + luaName;
        //Debug.Log(path);
        //self = (LuaTable)luaSvr.start(luaName);
        self = (LuaTable)luaSvr.start(luaName);

        try
        {
            update = (LuaFunction)self["update"];
        }
        catch (System.Exception)
        {   
        }

        try
        {
            fixUpdate = (LuaFunction)self["fixUpdate"];
        }
        catch (System.Exception)
        {
        }
        

        if (update != null)
            ud = update.cast<UpdateDelegate>();

        if (fixUpdate != null)
            fud = fixUpdate.cast<FixUpdateDelegate>();

        AddListenerToMono();


    }

    void MyUpdate() {
        if (ud != null) ud(self);
    }

    void MyFixUpdate() {
        if (fud != null) fud(self);
    }

    void AddListenerToMono() {
        if (ud != null) {
            MonoManager.GetInstance().AddUpdateListener(MyUpdate);
        }
        if (fud != null) {
            MonoManager.GetInstance().AddFixUpdateListener(MyFixUpdate);
        }
        
    }


    //¼ÓÔØLuaÎÄ¼þ
    public void LoadResLua()
    {
        string path= Application.dataPath + "/AssetBundles/lua";
        AssetBundle ab = AssetBundle.LoadFromFile(path);
        TextAsset[] text = ab.LoadAllAssets<TextAsset>();
        foreach (var txt in text)
        {
            if (File.Exists(Application.dataPath + "/Lua/" + txt.name + ".txt"))
            {
                File.Delete(Application.dataPath + "/Lua/" + txt.name + ".txt");
            }
            File.WriteAllBytes(Application.dataPath + "/Lua/" + txt.name + ".txt", txt.bytes);
        }
    }



}
