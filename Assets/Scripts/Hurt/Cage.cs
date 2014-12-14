using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 机关的父类
/// </summary>
public class Cage : KHurt
{

    private enum State { CLOSED, OPEN };

    private State _state = State.CLOSED;



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
    private float _speed = 1;



    //触发这个的按钮编号，事件源。
    private int _observer;


    void Start()
    {
        _trans = this.transform;
        _startPos = _trans.localPosition;
        _observer = (int)Properties[0];
        _speed = Properties[1];


        TriggerHurt[] triggers1 = this.transform.parent.GetComponentsInChildren<TriggerHurt>();

        foreach (var item in triggers1)
        {
            if (item.Id == _observer)
            {
                item.SwitchOnEvent += () =>
                {
                    if (_state == State.CLOSED)
                    {
                        _state = State.OPEN;
                    }
                };
            }
        }
    }

    void Update()
    {
        if (_state == State.OPEN)
        {
            _trans.Translate(_startPos.x, - (Time.deltaTime * _speed), _startPos.z,Space.World);

            if (_trans.position.y <= -Constant.DEADLINE_Y)
            {
                Destroy(gameObject);
            }
        }
    }
}