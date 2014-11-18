using UnityEngine;
using System.Collections;
using System;

public class FistHurt : KHurt
{

    private enum State { STOP, PUNCHING };


    /// <summary>
    /// 动画控制器
    /// </summary>
    private Animator _anim;

    private State _state = State.STOP;

    void Start()
    {
        _anim = GetComponent<Animator>();

        SwitchHurt s = this.transform.parent.GetComponentInChildren<SwitchHurt>();
        s.SwitchOnEvent += () =>
        {
            _state = State.PUNCHING;
            _anim.SetTrigger("punch");
        };
    }

    void Update()
    {

    }

    void OnAnimationOver()
    {
        _state = State.STOP;
    }
}
