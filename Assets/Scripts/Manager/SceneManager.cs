using UnityEngine;
using LitJson;
using System.Collections.Generic;

/// <summary>
/// 当前场景管理器。
/// </summary>
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
    /// 机关
    /// </summary>
    private List<KHurt> _hurts = new List<KHurt>();

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
        get { return GameManager.GetSceneConfByIndex(SceneIndex); }
    }

    /// <summary>
    /// 显示加载界面
    /// </summary>
    /// <param name="sceneIndex"></param>
    public static void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneIndex = sceneIndex;
        Application.LoadLevel(Constant.LEVEL_LOADING_NAME);
    }


    /// <summary>
    /// 加载场景内预制 
    /// </summary>
    private void LoadPrefab()
    {
        //加载场景
        GameObject scene =IOUtil.LoadGameObject("Map/Prefab/" + SceneConfig.prefab);
        scene.transform.rotation = Quaternion.identity;
        scene.transform.position = Vector3.zero;

        //加载玩家
        GameObject go = IOUtil.LoadGameObject("Sprites/Prefab/player");
        go.transform.parent = scene.transform.FindChild("Start");
        go.transform.rotation = Quaternion.identity;
        go.transform.localPosition = Vector3.zero;
        _player = go.GetComponent<Player>();

        //加载草莓
        go = IOUtil.LoadGameObject("Sprites/Prefab/strawberry"); ;
        go.transform.parent = scene.transform.FindChild("End");
        go.transform.rotation = Quaternion.identity;
        go.transform.localPosition = Vector3.zero;
        _strawberry = go.GetComponent<Strawberry>();

        //加载机关
        _hurts.Clear();
        foreach (var item in SceneConfig.hurt)
        {
            HurtConfigModel hurtConf = GameManager.GetHurtConfById(item.id);
            go = IOUtil.LoadGameObject("Sprites/Prefab/" + hurtConf.prefab);
            go.transform.parent = scene.transform.FindChild("Hurt");
            go.transform.rotation = Quaternion.identity;
            go.transform.localPosition = new Vector3(item.x, item.y, 0); ;
            KHurt hurt = go.GetComponentInChildren<KHurt>();
            hurt.Properties = hurtConf.value;
            if (hurt != null)
            {
                _hurts.Add(hurt);
            }
        }
    }

    public static void ShowResult(PlayResult result)
    {
        Time.timeScale = 0;
        KPresenter p = UIManager.Instance.Show(Constant.UI_RESULT);
        p.DataContent = result;
    }

    #region MonoBehaviour callback

    void OnLevelWasLoaded()
    {
        UIManager.Instance.HideAll();
        UIManager.Instance.Show(Constant.UI_GAME);

        LoadPrefab();
    }

    void OnApplicationQuit()
    {

    }

    #endregion
}
