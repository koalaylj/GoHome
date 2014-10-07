using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 机关的父类
/// </summary>
public class KHurt : MonoBehaviour
{
    /// <summary>
    /// 机关数值
    /// </summary>
    public virtual List<int> Properties
    {
        get;
        set;
    }
}