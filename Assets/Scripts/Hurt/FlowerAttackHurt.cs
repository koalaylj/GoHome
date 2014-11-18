using UnityEngine;
using System.Collections;

/// <summary>
/// 发射子弹类机关
/// </summary>
public class FlowerAttackHurt : KHurt
{
    private Animator _anim;
    private Transform _trans;
    private BoxCollider2D _collider;
    private SpriteRenderer _render;

    private float _fireRate;    //开火频率

    private float _delay;       //延迟时间

    private float _timeCount = 0;

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
        _trans = this.transform;
        _render = GetComponent<SpriteRenderer>();
        _delay = Properties[0];
        _fireRate = Properties[1];
    }

    void Update()
    {
        //根据帧动画动态调整碰撞框大小
        _collider.center = _render.sprite.bounds.center;
        _collider.size = new Vector2(_render.sprite.bounds.size.x * 0.75f, _render.sprite.bounds.size.y);

        _timeCount += Time.deltaTime;

        if (_timeCount >= _fireRate + _delay)
        {
            _anim.SetTrigger("eat");
            _timeCount = 0;
            _delay = 0;
        }
    }
}