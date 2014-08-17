using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject _strawberryPrefab;

    private Player _player;

    /// <summary>
    /// 要加载的场景
    /// </summary>
    private static string _scene_to_load;

    /// <summary>
    /// 显示加载界面
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(string sceneName)
    {
        _scene_to_load = sceneName;
        Application.LoadLevel("Loading");
    }

    private void LoadPrefab()
    {
        GameObject scenePrefab = Resources.Load("Map/Prefab/" + _scene_to_load) as GameObject;
        GameObject scene = GameObject.Instantiate(scenePrefab) as GameObject;
        scene.transform.rotation = Quaternion.identity;
        scene.transform.position = Vector3.zero;

        GameObject player = GameObject.Instantiate(_playerPrefab) as GameObject;
       
        player.transform.parent = scene.transform.FindChild("Start");
        player.transform.rotation = Quaternion.identity;
        player.transform.localPosition = Vector3.zero;

        _player = player.GetComponent<Player>();

        
    }


    #region MonoBehaviour callback
    void Start()
    {

    }

    void OnLevelWasLoaded()
    {
        UIManager.Instance.HideAll();
        UIManager.Instance.Show("Game");

        LoadPrefab();
    }


    void OnApplicationPause()
    {

    }


    void OnApplicationQuit()
    {

    }

    #endregion
}
