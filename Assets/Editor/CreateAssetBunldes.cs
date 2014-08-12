using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class CreateAssetBunldes
{
    [MenuItem("LazyFish/Create AssetBunldes")]
    static void CreateBunldes()
    {
        // AssetBundle 的目录名及扩展名
        string targetDir = "./Assets/StreamingAssets/Temp";
        string extensionName = ".assetbundle";

        //取得在 Project 视图中选择的资源(包含子目录中的资源)
        UnityEngine.Object[] SelectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);

        //建立存放 AssetBundle 的目录
        if (!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);

        foreach (UnityEngine.Object obj in SelectedAsset)
        {
            // 资源文件的路径
            string sourcePath = AssetDatabase.GetAssetPath(obj);

            // AssetBundle 存储路径
            string targetPath = Application.streamingAssetsPath + "/Temp/" + obj.name + extensionName;
            if (File.Exists(targetPath)) File.Delete(targetPath);

            //if (!(obj is GameObject) && !(obj is Texture2D) && !(obj is Material)) continue;

            //建立 AssetBundle
            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows))
            {
                Debug.Log(targetPath + " 建立完成");
            }
            else
            {
                Debug.LogError(obj.name + "建立失败");
            }
            AssetDatabase.Refresh ();
        }
    }
}