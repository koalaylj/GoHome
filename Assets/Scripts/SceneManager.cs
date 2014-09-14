using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    /// <summary>
    /// 玩家
    /// </summary>
    private Player _player;

    /// <summary>
    /// 草莓
    /// </summary>
    private Strawberry _strawberry;

    /// <summary>
    /// 场景配置文件
    /// </summary>
    private static Dictionary<int, SceneConfigModel> _sceneConf = new Dictionary<int, SceneConfigModel>();

    /// <summary>
    /// 机关配置文件
    /// </summary>
    private static Dictionary<int, HurtConfigModel> _hurtConf = new Dictionary<int, HurtConfigModel>();

    static SceneManager()
    {
        string json = IOUtil.LoadJson("scene.conf");
        List<SceneConfigModel> sceneConf = JsonMapper.ToObject<List<SceneConfigModel>>(json);

        foreach (var item in sceneConf)
        {
            _sceneConf[item.index] = item;
        }

        json = IOUtil.LoadJson("hurt.conf");
        List<HurtConfigModel> hurtConf = JsonMapper.ToObject<List<HurtConfigModel>>(json);

        foreach (var item in hurtConf)
        {
            _hurtConf[item.id] = item;
        }
    }

    /// <summary>
    /// 要加载的场景
    /// </summary>
    public static int SceneIndex
    {
        get;
        private set;
    }

    /// <summary>
    /// 显示加载界面
    /// </summary>
    /// <param name="sceneIndex"></param>
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
        GameObject scenePrefab = Resources.Load("Map/Prefab/" + _sceneConf[SceneIndex].prefab) as GameObject;
        GameObject scene = GameObject.Instantiate(scenePrefab) as GameObject;
        scene.transform.rotation = Quaternion.identity;
        scene.transform.position = Vector3.zero;

        //加载玩家
        Object prefab = Resources.Load("Sprites/Prefab/player");
        GameObject player = GameObject.Instantiate(prefab) as GameObject;
        player.transform.parent = scene.transform.FindChild("Start");
        player.transform.rotation = Quaternion.identity;
        player.transform.localPosition = Vector3.zero;
        _player = player.GetComponent<Player>();

        //加载草莓
        prefab = Resources.Load("Sprites/Prefab/strawberry");
        GameObject strawberry = GameObject.Instantiate(prefab) as GameObject;
        strawberry.transform.parent = scene.transform.FindChild("End");
        strawberry.transform.rotation = Quaternion.identity;
        strawberry.transform.localPosition = Vector3.zero;
        _strawberry = strawberry.GetComponent<Strawberry>();

        //TODO 加载机关

    }


    #region MonoBehaviour callback
    void Start()
    {
        Debug.Log("Start..");
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
