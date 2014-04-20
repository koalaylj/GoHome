using UnityEngine;
using System.Collections;


public enum AiState { NONE, PATROL, RUNNING, SHOOT }


public class Weasel : MonoBehaviour
{
    [SerializeField]
    private float Speed;    //巡逻速度

    [SerializeField]
    private Transform from_trans;   //从哪儿开始

    [SerializeField]
    private Transform to_trans;     //到哪儿结束

    private Vector2 target; //目标位置

    private bool facingRight = false;
    private Transform cache_trans;
    private Animator anim;

    private float distance;
    private AiState _state = AiState.NONE;

    private Vector2 from;
    private Vector2 to;

    //private int AiState = 0;

    //private int AiInterval = 30 * 5;

    void Start()
    {
        from = from_trans.position;
        to = to_trans.position;
        anim = GetComponent<Animator>();
        cache_trans = this.transform;
        target = to;
        distance = Vector2.Distance(from, to);
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (Vector3.Distance(cache_trans.position, from) >= distance)
        {
            Flip();
            target = from;
        }
        else if (Vector3.Distance(cache_trans.position, to) >= distance)
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
