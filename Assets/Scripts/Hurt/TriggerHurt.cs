using UnityEngine;
using System.Collections;
using System;

public class TriggerHurt : KHurt
{

    private enum State { ON, OFF };

    //按钮可点击的次数
    private int _clickTimesLimit = 0;

    //按钮的id
    private int _id;

    public int Id
    {
        get { return _id; }
    }


    /// <summary>
    /// 动画控制器
    /// </summary>
    private Animator _anim;

    private State _state = State.OFF;

    private int _switchCount = 0;

    public Action SwitchOnEvent;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        _id = (int)Properties[0];
        _clickTimesLimit = (int)Properties[1];
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(Constant.TAG_PLAYER) || other.tag.Equals(Constant.TAG_AI))
        {
            Debug.Log("dddddddddd");
            if (CanSwitch())
            {
                //可点击的次数
                if (_clickTimesLimit != 0)
                {
                    _switchCount++;
                }

                _state = State.ON;

                _anim.SetTrigger("push");

                if (SwitchOnEvent != null)
                {
                    SwitchOnEvent();
                }
            }
        }
    }

    void OnAnimationOver()
    {
        _state = State.OFF;
    }

    private bool CanSwitch()
    {
        if (_state == State.ON)
        {
            return false;
        }

        if (_clickTimesLimit != 0 && _switchCount >= _clickTimesLimit)
        {
            return false;
        }

        return true;
    }
}