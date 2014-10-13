using UnityEngine;
using System.Collections;

/// <summary>
/// 带刺风车机关
/// </summary>
public class CrossHurt : KHurt
{
    [SerializeField]
    private Transform _target;

    void Update()
    {
        _target.Rotate(Vector3.forward * Time.deltaTime * Properties[0]);
    }
}