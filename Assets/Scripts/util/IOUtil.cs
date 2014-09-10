using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// io相关工具类
/// </summary>
public class IOUtil
{
    public static string LoadJson(string fileName)
    {
        string json;
        var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        Debug.Log("Loading Json : " + filePath);

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
}