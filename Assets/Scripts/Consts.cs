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

    public static readonly string LEVEL_START_NAME = "Start";
    public static readonly string LEVEL_LOADING_NAME = "Loading";

    public static readonly int DEADLINE_Y = -20;


    #region tag

    public static readonly string TAG_PLAYER = "Player";
    public static readonly string TAG_HURT = "Hurt";
    public static readonly string TAG_AI = "AI";


    #endregion




    #region UI
    /// <summary>
    /// 主界面
    /// </summary>
    public static readonly string UI_MAIN = "Main";

    /// <summary>
    /// 游戏中的界面
    /// </summary>
    public static readonly string UI_GAME = "Game";

    /// <summary>
    /// 游戏结果界面
    /// </summary>
    public static readonly string UI_RESULT = "Result";

    /// <summary>
    /// 暂停界面
    /// </summary>
    public static readonly string UI_PAUSE = "Pause";

    #endregion

}