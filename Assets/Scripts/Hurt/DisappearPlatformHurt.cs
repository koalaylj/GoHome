using UnityEngine;
using System.Collections;

/// <summary>
/// 带刺风车机关
/// </summary>
public class DisappearPlatformHurt : KHurt
{
    /// <summary>
    /// 消失延迟移动的时间
    /// </summary>
    private float _delay;

    void Start()
    {
        _delay = Properties[0];
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == Constant.TAG_PLAYER)
        {
            StartCoroutine("WaitAndDestroy", _delay);
        }
    }

    /// <summary>
    /// 等一段时间销毁一个物体
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator WaitAndDestroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }

}
