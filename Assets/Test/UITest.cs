using UnityEngine;
using System.Collections;

public class UITest : MonoBehaviour
{
    void Start()
    {
        // Debug.Log(Application.dataPath);
        // Debug.Log(Application.persistentDataPath);
        // Debug.Log(Application.platform);
    }

    void OnGUI()
    {
        if (GUILayout.Button("打开主界面"))
        {
            UIManager.Instance.Show("Main");
        }
    }

    void Update()
    {

    }
}
