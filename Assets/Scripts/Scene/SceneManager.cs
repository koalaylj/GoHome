using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    void Start()
    { }

    void OnLevelWasLoaded() {
        UIManager.Instance.HideAll();
        UIManager.Instance.Show("Game");
    }
}
