using UnityEngine;
using System.Collections;
using System;

public class Strawberry : MonoBehaviour
{

    /// <summary>
    /// 关卡结束事件 时间到 或者刺猬吃到草莓
    /// T1:过关结果
    /// </summary>
    public Action<PlayResult> OnOverEvent;

    //public Action

    /// <summary>
    /// 过关结果
    /// </summary>
    private PlayResult _result = PlayResult.STAR3;

    /// <summary>
    /// 草莓是否被虫子咬了
    /// </summary>
    private bool _isEatenByBug = false;

    /// <summary>
    /// 计时器
    /// </summary>
    private float _tick = 0f;


    /// <summary>
    /// 过关总时间
    /// </summary>
    private int _totalTime = 0;


    /// <summary>
    /// 是否开始计时
    /// </summary>
    private bool _tickerStarted = false;

    /// <summary>
    /// 开始计时
    /// </summary>
    /// <param name="totalTime"></param>
    public void StartTick(int totalTime)
    {
        _tickerStarted = true;
        _totalTime = totalTime;
    }

    void Update()
    {
        if (_tickerStarted)
        {
            _tick += Time.deltaTime;
            if (_tick >= _totalTime && OnOverEvent != null)
            {
                OnOverEvent(PlayResult.FAIL);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_isEatenByBug)
            {
                _result = PlayResult.STAR2;
            }
            else
            {
                _result = PlayResult.STAR3;
            }

            if (OnOverEvent != null)
            {
                OnOverEvent(_result);
            }
            this.gameObject.SetActive(false);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(100, 40, 100, 30), "time:" + _tick.ToString());
    }

    void OnAnimationOver()
    {
        _isEatenByBug = true;
    }
}
