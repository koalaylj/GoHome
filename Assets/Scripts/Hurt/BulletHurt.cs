using UnityEngine;
using System.Collections;

/// <summary>
/// 子弹
/// </summary>
public class BulletHurt : KHurt
{
    public float Speed { get; set; }

    private float _lifeTime = 30;//生命时间 单位：秒

    private Transform _trans;

    private float _timeCount = 0;

    void Start()
    {
        _trans = transform;
    }

    void Update()
    {
        _timeCount += Time.deltaTime;

        //超过一定时间自动消亡
        if (_timeCount >= _lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        _trans.Translate(_trans.right * Time.deltaTime * Speed, Space.World);
    }
}