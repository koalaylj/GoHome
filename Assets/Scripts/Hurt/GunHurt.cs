using UnityEngine;
using System.Collections;

/// <summary>
/// 发射子弹类机关
/// </summary>
public class GunHurt : KHurt
{
    [SerializeField]
    private BulletGenerator _bulletGenerator;

    /// <summary>
    /// 动画控制器
    /// </summary>
    private Animator _anim;

    private Transform _trans;

    private float _fireRate;    //开火频率
    private float _bulletSpeed; //子弹速度
    private float _delay;

    private float _timeCount = 0;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _trans = this.transform;
        _fireRate = Properties[0];
        _bulletSpeed = Properties[1];
        _delay = Properties[2];
    }

    void Update()
    {
        _timeCount += Time.deltaTime;

        if (_timeCount >= _fireRate + _delay)
        {
            if (_anim != null)
            {
                _anim.SetTrigger("shoot");
            }
            else
            {
                OnShoot();
            }
            _timeCount = 0;
            _delay = 0;
        }
    }

    void OnShoot()
    {
        _bulletGenerator.GenerateBullet(_bulletSpeed);
    }
}