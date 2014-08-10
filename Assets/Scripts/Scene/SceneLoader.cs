using UnityEngine;
using System.Collections;

public class SceneLoader {

    public static string SCENE_TO_LOAD;

    public static void LoadScene(string sceneName) {
        
        SCENE_TO_LOAD = sceneName;

        Application.LoadLevel("Loading");

    }

}