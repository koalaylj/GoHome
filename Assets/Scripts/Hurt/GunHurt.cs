using UnityEngine;
using System.Collections;

/// <summary>
/// 发射子弹类机关
/// </summary>
public class GunHurt : KHurt
{
    [SerializeField]
    private BulletGenerator _bulletGenerator;

    private Transform _trans;

    private float _fireRate;    //开火频率
    private float _bulletSpeed; //子弹速度

    private float _timeCount = 0;

    void Start()
    {
        _trans = this.transform;
        _fireRate = Properties[0];
        _bulletSpeed = Properties[1];
    }

    void Update()
    {
        _timeCount += Time.deltaTime;

        if (_timeCount >= _fireRate)
        {
            _bulletGenerator.GenerateBullet(_bulletSpeed);
            _timeCount = 0;
        }
    }
}