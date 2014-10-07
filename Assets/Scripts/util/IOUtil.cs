using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// io工具类
/// 作者：于小懒
/// </summary>
public class IOUtil
{
    /// <summary>
    /// 存档名字
    /// </summary>
    private readonly static string PREF_NAME = "com.github.koalaylj";

    /// <summary>
    /// windows下存档的位置,测试方便。
    /// %userprofile%/AppData/LocalLow/LazyFish/my_strawberry
    /// </summary>
    private readonly static string PREF_NAME_WINDOWS = Path.Combine(Application.persistentDataPath, "my-strawberry.json");

    /// <summary>
    /// 从StreamingAssets目录下记载文本文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string LoadText(string fileName)
    {
        string json;
        var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        Debug.Log("Loading text file : " + filePath);

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
        Debug.Log("保存存档");

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            File.WriteAllText(PREF_NAME_WINDOWS, text, System.Text.Encoding.UTF8);
        }
        else
        {
            PlayerPrefs.SetString(PREF_NAME, text);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// 加载存档
    /// </summary>
    /// <returns></returns>
    public static string LoadPrefs()
    {
        Debug.Log("加载存档");

        string text = "";

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (File.Exists(PREF_NAME_WINDOWS))
            {
                text = File.ReadAllText(PREF_NAME_WINDOWS, System.Text.Encoding.UTF8);
            }
        }
        else
        {
            text = PlayerPrefs.GetString(PREF_NAME, "");
        }

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
        Debug.Log("Loading prefab:" + prefabName);
        GameObject prefab = Resources.Load(prefabName) as GameObject;
        return prefab;
    }
}