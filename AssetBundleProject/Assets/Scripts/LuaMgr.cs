using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SLua;

/// <summary>
/// Lua管理器
/// </summary>
public class LuaMgr : BaseManager<LuaMgr>
{
    private static LuaSvr luaSvr;
    private LuaFunction luaFunction;

    public LuaSvr Init()
    {
        if (luaSvr != null) {
            return luaSvr;
        }
        //唯一的解析器
        luaSvr = new LuaSvr();
        LuaSvr.mainState.loaderDelegate = MyCustomLoader;
        
        luaSvr.init(null,()=> {
            Debug.Log("初始化");
        });

        return luaSvr;

    }

    

    private byte[] MyCustomLoader(string filepath,ref string ab)
    {
        //测试传入的参数是什么
        Debug.Log(filepath);
        //决定Lua文件所在路径
        string path = Application.dataPath + "/Lua/" + filepath + ".txt";
        //C#自带的文件读取类
        if (File.Exists(path))
        {
            return File.ReadAllBytes(path);
        }
        else
            Debug.Log("MyCustomLoader重定向失败");

        return null;
    }

    //再写一个Load 用于从AB包加载Lua文件
    private byte[] MyCustomLoaderFormAB(ref string filepath)
    {
        //改为我们的AB包管理器加载
        TextAsset file2 = ABMgr.GetInstance().LoadRes<TextAsset>("lua", filepath + ".lua");
        if (file2 != null)
            return file2.bytes;
        else
            Debug.Log("MyCustomLoaderFormAB重定向失败");
        return null;
    }



    
}
