using UnityEngine;
using System.Collections;
using System.IO;

public class IOTool {

    public static string LoadJson(string fileName) {
       
        string json;
        var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        Debug.Log("Loading Json : " + filePath);


        if (filePath.Contains("://")) {
            var www = new WWW(filePath);
            while (!www.isDone) {
            }
            json = www.text;
        }
        else
            json = System.IO.File.ReadAllText(filePath);

        return json;

//#if UNITY_ANDROID
//        //string path =Application.streamingAssetsPath + "/" +  fileName;
//        WWW www = new WWW(path);
//        while (!www.isDone) {
//        }
//        json = www.text;
//#else
        
//        if (!File.Exists(path)) {
//            Debug.LogError("文件不存在：" + path);
//        }

//        json = File.ReadAllText(path);

//#endif

        return json;
    }
}
