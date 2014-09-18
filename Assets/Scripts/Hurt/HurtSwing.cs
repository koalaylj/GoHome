using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 机关的父类
/// </summary>
public class HurtSwing : Hurt
{
    /// <summary>
    /// 初始位置
    /// </summary>
    private Vector3 _startPos;

    /// <summary>
    /// transform 缓存
    /// </summary>
    private Transform _trans;

    /// <summary>
    /// 速度
    /// </summary>
    private float _speed;

    /// <summary>
    /// 移动范围
    /// </summary>
    private float _moveRange;


    /// <summary>
    /// 机关延迟开始时间
    /// </summary>
    private float _delay;


    /// <summary>
    /// 是否开始
    /// </summary>
    private bool _started = false;

    IEnumerator Start()
    {
        _trans = this.transform;
        _startPos = _trans.localPosition;
        _delay = Properties[0];
        _speed = Properties[1];
        _moveRange = Properties[2];

        yield return new WaitForSeconds(_delay);
        _started = true;
    }

    float y = 0;
    void Update()
    {
        if (_started)
        {
            y = (y + Time.deltaTime * _speed);

            _trans.position = new Vector3(_startPos.x, _startPos.y - Mathf.PingPong(y, _moveRange), _startPos.z);
        }
    }
}