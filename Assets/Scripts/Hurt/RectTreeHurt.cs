using UnityEngine;
using System.Collections;

/// <summary>
/// 带刺风车机关
/// </summary>
public class RectTreeHurt : KHurt
{
    [SerializeField]
    private Transform _movingPlatform;

    /// <summary>
    /// 移动平台目标位置
    /// </summary>
    private Vector3 _endPoint;

    /// <summary>
    /// 移动平台的移动速度
    /// </summary>
    private float _speed;

    /// <summary>
    /// 移动平台延迟移动的时间
    /// </summary>
    private float _delay;

    /// <summary>
    /// 是否开始移动
    /// </summary>
    private bool _started = false;


    IEnumerator Start()
    {
        _delay = Properties[0];
        _speed = Properties[1];
        _endPoint = new Vector3(_movingPlatform.position.x + Properties[2], _movingPlatform.position.y, _movingPlatform.position.z);

        yield return new WaitForSeconds(_delay);

        _started = true;
    }


    void Update()
    {
        if (_started)
        {
            _movingPlatform.position = Vector3.Lerp(_movingPlatform.position, _endPoint, Time.deltaTime * _speed);
        }
    }
}
