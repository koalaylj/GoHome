using UnityEngine;
using System.Collections;

public class Helper : MonoBehaviour
{

    void Start()
    {
        Application.targetFrameRate = 30;
    }

    void OnGUI()
    {
        //GUILayout.Button(Application.targetFrameRate.ToString());
        //GUI.Button(new Rect(10, 10, 80, 30), 1 / Time.deltaTime + "");
    }

    void Update()
    {

    }
}
