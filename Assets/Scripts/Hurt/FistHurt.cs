using UnityEngine;
using System.Collections;
using System;

public class FistHurt : KHurt
{

    private enum State { STOP, PUNCHING };

    //触发这个拳头的按钮编号，事件源。
    private int _observer;


    /// <summary>
    /// 动画控制器
    /// </summary>
    private Animator _anim;

    private State _state = State.STOP;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _observer = (int)Properties[0];
        SwitchHurt[] switches = this.transform.parent.GetComponentsInChildren<SwitchHurt>();

        foreach (var item in switches)
        {
            if (item.Id == _observer)
            {
                item.SwitchOnEvent += () =>
                {
                    _state = State.PUNCHING;
                    _anim.SetTrigger("punch");
                };
            }
        }
    }

    void Update()
    {

    }

    void OnAnimationOver()
    {
        _state = State.STOP;
    }
}
