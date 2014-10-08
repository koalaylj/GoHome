using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 场景配置
/// </summary>
public class SceneConfigModel
{
    /// <summary>
    /// 关卡索引
    /// </summary>
    public int index { get; set; }

    /// <summary>
    /// 关卡的名字
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// 关卡预制名字
    /// </summary>
    public string prefab { get; set; }

    /// <summary>
    /// 过关时间限制 单位 秒 三个星级
    /// </summary>
    public int star1 { get; set; }
    public int star2 { get; set; }
    public int star3 { get; set; }

    /// <summary>
    /// 关卡中的机关
    /// </summary>
    public List<SceneHurtConfigModel> hurt { get; set; }
}

public class SceneHurtConfigModel
{
    public int id { get; set; }

    public float x { get; set; }

    public float y { get; set; }
}

/// <summary>
/// 机关配置
/// </summary>
public class HurtConfigModel
{

    public int id { get; set; }

    /// <summary>
    /// 机关类型
    /// </summary>
    public int type { get; set; }

    /// <summary>
    /// 机关名字 
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// 预制名字
    /// </summary>
    public string prefab { get; set; }

    /// <summary>
    /// 机关数值
    /// 朝向 0:left 1:up 2:right 3:down
    /// </summary>
    public List<float> value { get; set; }
}