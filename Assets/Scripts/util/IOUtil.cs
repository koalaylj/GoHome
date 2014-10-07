using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// io工具类
/// 作者：于小懒
/// </summary>
public class IOUtil
{

#if UNITY_STANDALONE || UNITY_EDITOR
    /// <summary>
    /// windows/mac/linux 下的存档。
    /// windows: %userprofile%/AppData/LocalLow/LazyFish/my_strawberry
    /// </summary>   
    private readonly static string PREF_NAME = Path.Combine(Application.persistentDataPath, "my-strawberry.json");
#else
    /// <summary>
    /// 移动设备存档
    /// </summary>
    private readonly static string PREF_NAME = "com.github.koalaylj";
#endif

    /// <summary>
    /// 从StreamingAssets目录下记载文本文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string LoadText(string fileName)
    {
        string json;
        var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
       // Debug.Log("Loading text file : " + filePath);

        if (filePath.Contains("://"))
        {
            var www = new WWW(filePath);
            while (!www.isDone)
            {
            }
            json = www.text;
        }
        else
            json = System.IO.File.ReadAllText(filePath);

        return json;
    }

    /// <summary>
    /// 保存文本形式的存档(json形式的文本比较方便)
    /// Windows 存档位置注册表的 HKCU\Software\[company name]\[product name]键下(这里company和product名是在Project Setting中设置的.)
    /// </summary>
    /// <param name="text"></param>
    public static void SavePrefs(string text)
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        File.WriteAllText(PREF_NAME, text, System.Text.Encoding.UTF8);
#else
        PlayerPrefs.SetString(PREF_NAME, text);
        PlayerPrefs.Save();
#endif

        Debug.Log("存档已保存");

    }

    /// <summary>
    /// 加载存档
    /// </summary>
    /// <returns></returns>
    public static string LoadPrefs()
    {
        Debug.Log("加载存档");

        string text = "";

#if UNITY_STANDALONE || UNITY_EDITOR
        if (File.Exists(PREF_NAME))
        {
            text = File.ReadAllText(PREF_NAME, System.Text.Encoding.UTF8);
        }
#else
        text = PlayerPrefs.GetString(PREF_NAME, "");
#endif
        return text;
    }

    /// <summary>
    /// 从Resouce目录下 通过预制名字 加载预制
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public static GameObject LoadGameObject(string prefabName)
    {
        Object prefab = LoadPrefab(prefabName);
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        //Resources.UnloadUnusedAssets();
        return go;
    }

    /// <summary>
    /// 加载prefab
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public static GameObject LoadPrefab(string prefabName)
    {
//        Debug.Log("Loading prefab:" + prefabName);
        GameObject prefab = Resources.Load(prefabName) as GameObject;
        return prefab;
    }
}