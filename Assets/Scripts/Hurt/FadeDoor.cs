using UnityEngine;
using System.Collections;

public class FadeDoor : KHurt
{
    private enum State { CLOSED, OPEN };

    private State _state = State.CLOSED;

    private Color _color;

    //消失时间
    private float _fadeTime = 1;

    //触发这个拳头的按钮编号，事件源。
    private int _observer;


    void Start()
    {
        _color = this.renderer.material.color;
        _observer = (int)Properties[0];
        _fadeTime = Properties[1];

        TriggerHurt[] triggers1 = this.transform.parent.GetComponentsInChildren<TriggerHurt>();
        SwitchHurt[] triggers2 = this.transform.parent.GetComponentsInChildren<SwitchHurt>();


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

        foreach (var item in triggers2)
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

    float timeCount = 0;
    void Update()
    {
        if (_state == State.OPEN)
        {
            timeCount += Time.deltaTime;
            float alpha = Mathf.Lerp(1,0,timeCount/_fadeTime);
            this.renderer.material.color = new Color(_color.r, _color.g, _color.a, alpha);

            if (alpha <= 0.01)
            {
                Destroy(gameObject);
            }
        }
    }
}

