using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System;

/// <summary>
/// 游戏管理器 需要最先初始化
///     1.配置文件
///     2.存档
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 场景配置文件
    /// <sceneIndex,conf>
    /// </summary>
    private static Dictionary<int, SceneConfigModel> _sceneConf = new Dictionary<int, SceneConfigModel>();

    /// <summary>
    /// 机关配置文件
    /// <hurtId,conf>
    /// </summary>
    private static Dictionary<int, HurtConfigModel> _hurtConf = new Dictionary<int, HurtConfigModel>();

    /// <summary>
    /// 游戏存档
    /// </summary>
    private static PrefsModel _prefs;

    /// <summary>
    /// 通过场景id获取场景配置信息
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static SceneConfigModel GetSceneConfByIndex(int index)
    {
        if (_sceneConf.ContainsKey(index))
        {
            return _sceneConf[index];
        }

        return null;
    }

    /// <summary>
    /// 通过机关id加载机关配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static HurtConfigModel GetHurtConfById(int id)
    {
        if (_hurtConf.ContainsKey(id))
        {
            return _hurtConf[id];
        }

        return null;
    }

    /// <summary>
    /// 获取关卡存档信息
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static MissionPrefsModel GetMissionPrefsByIndex(int index)
    {
        foreach (var item in _prefs.Missions)
        {
            if (item.Index == index)
            {
                return item;
            }
        }

        throw new Exception("木有此关卡：" + index);
    }

    /// <summary>
    /// 所有关卡存档
    /// </summary>
    public static ICollection<int> AllMissions
    {
        get
        {
            //List<int> missions = new List<int>();
            //missions.AddRange(_sceneConf.Keys);
            //return missions;
            return _sceneConf.Keys;
        }
    }

    /// <summary>
    /// 打开游戏
    /// </summary>
    void Start()
    {
        Initialize();
        UIManager.Instance.Show("Main");
    }

    /// <summary>
    /// 游戏初始化
    /// </summary>
    //static GameManager()
    private static void Initialize()
    {
        //加载场景配置文件
        string json = IOUtil.LoadText("scene.conf");
        List<SceneConfigModel> sceneConf = JsonMapper.ToObject<List<SceneConfigModel>>(json);

        foreach (var item in sceneConf)
        {
            _sceneConf[item.index] = item;
        }

        //加载机关配置文件
        json = IOUtil.LoadText("hurt.conf");
        List<HurtConfigModel> hurtConf = JsonMapper.ToObject<List<HurtConfigModel>>(json);

        foreach (var item in hurtConf)
        {
            _hurtConf[item.id] = item;
        }

        //加载存档
        json = IOUtil.LoadPrefs();
        if (string.IsNullOrEmpty(json))
        {
            //如果没有存档则初始化存档

            Debug.Log("初始化存档");

            _prefs = new PrefsModel();
            _prefs.Missions = new List<MissionPrefsModel>();

            foreach (var item in _sceneConf)
            {
                MissionPrefsModel mission = new MissionPrefsModel() { Index = item.Key };
                if (mission.Index == 1)
                {
                    mission.Star = 2;
                    mission.Lock = false;
                }

                _prefs.Missions.Add(mission);
            }


            IOUtil.SavePrefs(JsonMapper.ToJson(_prefs));
        }
        else
        {
            _prefs = JsonMapper.ToObject<PrefsModel>(json);
        }
    }


}