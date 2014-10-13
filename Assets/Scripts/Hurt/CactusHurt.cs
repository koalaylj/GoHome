using UnityEngine;
using System.Collections;

/// <summary>
/// 仙人球机关
/// </summary>
public class CactusHurt : KHurt
{
    [SerializeField]
    private BulletGenerator[] _guns;

    private Transform _trans;

    private float _rotateSpeed; //旋转速度
    private float _fireRate;    //开火频率
    private float _bulletSpeed; //子弹速度

    private float _timeCount = 0;

    void Start()
    {
        _trans = this.transform;
        _rotateSpeed = Properties[0];
        _fireRate = Properties[1];
        _bulletSpeed = Properties[2];
    }

    void Update()
    {
        _timeCount += Time.deltaTime;

        if (_timeCount >= _fireRate)
        {
            foreach (var item in _guns)
            {
                item.Speed = _bulletSpeed;
                item.GenerateBullet();
            }

            _timeCount = 0;
        }

        _trans.Rotate(Vector3.forward * Time.deltaTime * _rotateSpeed);
    }
}