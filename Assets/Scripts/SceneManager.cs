using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private GameObject _strawberryPrefab;

    private Player _player;
    private Strawberry _strawberry;

    /// <summary>
    /// 要加载的场景
    /// </summary>
    public static int SceneIndex
    {
        get;
        private set;
    }

    private string _sceneName
    {
        get { return "Scene_1_" + SceneIndex; }
    }

    /// <summary>
    /// 显示加载界面
    /// </summary>
    /// <param name="sceneName"></param>
    public static void LoadScene(int sceneIndex)
    {
        SceneIndex = sceneIndex;
        Application.LoadLevel("Loading");
    }


    /// <summary>
    /// 加载场景内预制 
    /// </summary>
    private void LoadPrefab()
    {
        //加载场景
        GameObject scenePrefab = Resources.Load("Map/Prefab/" + _sceneName) as GameObject;
        GameObject scene = GameObject.Instantiate(scenePrefab) as GameObject;
        scene.transform.rotation = Quaternion.identity;
        scene.transform.position = Vector3.zero;

        //加载玩家
        GameObject player = GameObject.Instantiate(_playerPrefab) as GameObject;

        player.transform.parent = scene.transform.FindChild("Start");
        player.transform.rotation = Quaternion.identity;
        player.transform.localPosition = Vector3.zero;
        _player = player.GetComponent<Player>();

        //加载草莓
        GameObject strawberry = GameObject.Instantiate(_strawberryPrefab) as GameObject;
        strawberry.transform.parent = scene.transform.FindChild("End");
        strawberry.transform.rotation = Quaternion.identity;
        strawberry.transform.localPosition = Vector3.zero;
        _strawberry = strawberry.GetComponent<Strawberry>();

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
