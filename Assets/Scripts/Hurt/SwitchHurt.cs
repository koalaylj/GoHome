using UnityEngine;
using System.Collections;
using System;

public class SwitchHurt : KHurt
{

    private enum State { ON, OFF };


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
    }


    void OnTap(TapGesture gesture)
    {
        if (gesture.Selection == this.gameObject)
        {
            if (CanSwitch())
            {
                //可点击的次数
                if (Properties[0] != 0)
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

        if (Properties[0] != 0 && _switchCount >= Properties[0])
        {
            return false;
        }

        return true;
    }
}
