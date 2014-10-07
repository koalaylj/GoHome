using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Prefs 存储 游戏存档
/// </summary>
public class PrefsModel
{
    /// <summary>
    /// 新手教程是否完成
    /// </summary>
    public bool TutorialCompleted { get; set; }

    /// <summary>
    /// 关卡中的机关
    /// </summary>
    public List<MissionPrefsModel> Missions { get; set; }
}

/// <summary>
/// 机关配置
/// </summary>
public class MissionPrefsModel
{
    /// <summary>
    /// 关卡id & index
    /// </summary>
    public int Index { get; set; }


    private int _star = 0;

    /// <summary>
    /// 星级
    /// </summary>
    public int Star
    {
        get { return _star; }
        set { _star = value; }
    }

    private bool _lock = true;

    /// <summary>
    /// 是否锁定
    /// </summary>
    public bool Lock
    {
        get { return _lock; }
        set { _lock = value; }
    }
}