using UnityEngine;
using System.Collections;

public class RotateDoor : KHurt
{
    private enum State { CLOSED, OPEN };

    private State _state = State.CLOSED;

    private Transform _trans;


    //要旋转的角度
    private Quaternion _targetRotation;

    //旋转速度
    private float _speed;

    //触发这个拳头的按钮编号，事件源。
    private int _observer;



    // Use this for initialization
    void Start()
    {
        _trans = this.transform;


        _observer = (int)Properties[0];
        _speed = Properties[1];
        _targetRotation = Quaternion.Euler(_trans.rotation.x, _trans.rotation.y, _trans.rotation.z + Properties[2]);


        TriggerHurt[] triggers = this.transform.parent.GetComponentsInChildren<TriggerHurt>();

        foreach (var item in triggers)
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
            //float angle = Mathf.LerpAngle(0, _to, Time.time );
            //transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,angle);      

            transform.rotation = Quaternion.RotateTowards(_trans.rotation, _targetRotation, Time.deltaTime * _speed);
        }
    }
}