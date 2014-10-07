using UnityEngine;
using System.Collections;

/// <summary>
/// 过关结果 星级
/// </summary>
public enum PlayResult
{
    FAIL = 0,
    STAR1 = 1,
    STAR2 = 2,
    STAR3 = 3
}

public static class Constant
{
    // public static readonly int MAX_MISSION = 24;
    public static readonly string LEVEL_START_NAME = "Start";
    public static readonly string LEVEL_LOADING_NAME = "Loading";
}