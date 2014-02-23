using UnityEngine;
using System.Collections;

public class Caterpillar : MonoBehaviour
{

    [SerializeField]
    private float range = 2;

    [SerializeField]
    private float Speed;

    private Vector2 from;
    private Vector2 to;
    private Vector2 target;

    private bool facingRight = false;
    private Transform cacheTrans;
    private Animator anim;	

    void Start()
    {
        anim = GetComponent<Animator>();
        cacheTrans = this.transform;
        from = new Vector2(cacheTrans.position.x + range / 2, cacheTrans.position.y);
        to = new Vector2(cacheTrans.position.x - range / 2, cacheTrans.position.y);
        target = to;
        //GameObject a = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //a.transform.position = from;
        //a.transform.localScale = Vector3.one * 0.2f;
        //GameObject b = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //b.transform.position = to;
        //b.transform.localScale = Vector3.one * 0.2f;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (Vector3.Distance(cacheTrans.position, from) >= range)
        {
            Flip();
            target = from;
        }
        else if (Vector3.Distance(cacheTrans.position, to) >= range)
        {
            Flip();
            target = to;
        }
        cacheTrans.position = Vector2.MoveTowards(cacheTrans.position, target, Time.deltaTime * Speed);
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
