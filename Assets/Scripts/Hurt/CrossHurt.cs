using UnityEngine;
using System.Collections;

/// <summary>
/// 旋转机关
/// </summary>
public class CrossHurt : KHurt
{
    [SerializeField]
    private Transform _target;

    void Start()
    {
        if (_target == null)
        {
            _target = this.transform;
        }
    }

    void Update()
    {
        _target.Rotate(Vector3.forward * Time.deltaTime * Properties[0] * Properties[1]);
    }
}