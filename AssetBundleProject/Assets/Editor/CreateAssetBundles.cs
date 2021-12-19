using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles {

	//创建菜单栏
	[MenuItem("AssetBundle/Build AssetBundles")]
	//打包
	static void BuildAllAssetBundle()
	{
		//打包路径
		string path = "Assets/AssetBundles";
		if (Directory.Exists(path) == false)
		{
			//在工程下创建AssetBundles目录
			Directory.CreateDirectory(path);
		}
        //打出AB包：路径，压缩算法：默认LZMA,LZ4(ChunkBasedCompression),平台的目标
        //BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows64);
    }
}
