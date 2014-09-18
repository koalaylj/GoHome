using UnityEngine;
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

    private List<Hurt> _hurts = new List<Hurt>();

    /// <summary>
    /// 场景配置文件
    /// </summary>
    private static Dictionary<int, SceneConfigModel> _sceneConf = new Dictionary<int, SceneConfigModel>();

    /// <summary>
    /// 机关配置文件
    /// </summary>
    private static Dictionary<int, HurtConfigModel> _hurtConf = new Dictionary<int, HurtConfigModel>();


    [SerializeField]
    private bool _debug;

    [SerializeField]
    private int _index;

    void Start()
    {
        if (_debug)
        {
            SceneIndex = _index;
            OnLevelWasLoaded();
        }
    }

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
    /// 当前关卡的配置
    /// </summary>
    public static SceneConfigModel SceneConfig
    {
        get { return _sceneConf[SceneIndex]; }
    }

    /// <summary>
    /// 显示加载界面
    /// </summary>
    /// <param name="sceneIndex"></param>
    public static void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneIndex = sceneIndex;
        Application.LoadLevel("Loading");
    }


    /// <summary>
    /// 加载场景内预制 
    /// </summary>
    private void LoadPrefab()
    {
        //加载场景
        GameObject scene = LoadGameObject("Map/Prefab/" + SceneConfig.prefab);
        scene.transform.rotation = Quaternion.identity;
        scene.transform.position = Vector3.zero;

        //加载玩家
        GameObject go = LoadGameObject("Sprites/Prefab/player");
        go.transform.parent = scene.transform.FindChild("Start");
        go.transform.rotation = Quaternion.identity;
        go.transform.localPosition = Vector3.zero;
        _player = go.GetComponent<Player>();

        //加载草莓
        go = LoadGameObject("Sprites/Prefab/strawberry"); ;
        go.transform.parent = scene.transform.FindChild("End");
        go.transform.rotation = Quaternion.identity;
        go.transform.localPosition = Vector3.zero;
        _strawberry = go.GetComponent<Strawberry>();

        //加载机关
        _hurts.Clear();
        foreach (var item in SceneConfig.hurt)
        {
            HurtConfigModel hurtConf = _hurtConf[item.id];
            go = LoadGameObject("Sprites/Prefab/" + hurtConf.prefab);
            go.transform.parent = scene.transform.FindChild("Hurt");
            go.transform.rotation = Quaternion.identity;
            go.transform.localPosition = new Vector3(item.x, item.y, 0); ;
            Hurt hurt = go.GetComponent<Hurt>();
            hurt.Properties = hurtConf.value;
            if (hurt != null)
            {
                _hurts.Add(hurt);
            }
        }
    }

    private GameObject LoadGameObject(string prefabName)
    {
        Debug.Log("Loading prefab:" + prefabName);
        Object prefab = Resources.Load(prefabName);
        GameObject go = GameObject.Instantiate(prefab) as GameObject;
        Resources.UnloadUnusedAssets();
        return go;
    }


    public static void ShowResult(PlayResult result)
    {
        Time.timeScale = 0;
        Presenter p = UIManager.Instance.Show("Result");
        p.DataContent = result;
    }

    #region MonoBehaviour callback

    void OnLevelWasLoaded()
    {
        UIManager.Instance.HideAll();
        UIManager.Instance.Show("Game");

        LoadPrefab();
    }

    void OnApplicationQuit()
    {

    }

    #endregion
}
