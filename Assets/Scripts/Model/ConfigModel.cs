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

    /// <summary>
    /// 旋转角度
    /// </summary>
    public float rotation { get; set; }

    /// <summary>
    /// 替换机关配置文件中的value
    /// </summary>
    public List<float> value { get; set; }
}

/// <summary>
/// 机关配置
/// </summary>
public class HurtConfigModel
{

    /// <summary>
    /// 机关id
    /// 四位数：AABB AA为类型
    /// </summary>
    public int id { get; set; }

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
    /// </summary>
    public List<float> value { get; set; }

    /// <summary>
    /// 机关描述
    /// </summary>
    public List<string> desc { get; set; }

    public override string ToString()
    {
        return string.Format("id:{0},name:{1},prefab:{2},value:{3},desc:{4}", id, name, prefab, value, desc);
    }

}