using UnityEngine;
using System.Collections;

/// <summary>
/// 子弹
/// </summary>
public class StoneHurt : KHurt
{
    /// <summary>
    /// 质量
    /// </summary>
    private float _weight = 1;

    void Start()
    {
        _weight = Properties[0];
        rigidbody2D.mass = _weight;
    }


    void Update()
    {
        //自动消亡
        if (this.transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }
}