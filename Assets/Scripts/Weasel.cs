using UnityEngine;
using System.Collections;

public class Weasel : MonoBehaviour
{
    [SerializeField]
    private float range = 2;//巡逻区域

    [SerializeField]
    private float Speed;//巡逻速度

    private Vector2 from;   //从哪儿开始
    private Vector2 to;     //到哪儿结束
    private Vector2 target; //目标位置

    private bool facingRight = false;
    private Transform cache_trans;
    private Animator anim;

    private int ai_time;

    void Start()
    {
        anim = GetComponent<Animator>();
        cache_trans = this.transform;
        from = new Vector2(cache_trans.position.x + range / 2, cache_trans.position.y);
        to = new Vector2(cache_trans.position.x - range / 2, cache_trans.position.y);
        target = to;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (Vector3.Distance(cache_trans.position, from) >= range)
        {
            Flip();
            target = from;
        }
        else if (Vector3.Distance(cache_trans.position, to) >= range)
        {
            Flip();
            target = to;
        }
        cache_trans.position = Vector2.MoveTowards(cache_trans.position, target, Time.deltaTime * Speed);
    }

    /// <summary>
    /// 转身
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
