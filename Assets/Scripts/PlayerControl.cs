using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    private bool facingRight = true;
    private bool jump = false;

    [SerializeField]
    private float moveForce = 300;

    [SerializeField]
    private float maxSpeed = 500f;

    [SerializeField]
    private float jumpForce = 500;

    private Transform groundCheck;			// A position marking where to check if the player is grounded.
    private bool grounded = false;			// Whether or not the player is grounded.
    private Animator anim;					// Reference to the player's animator component.

    void Awake()
    {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    void FixedUpdate()
    {

        //   if (Input.GetKey(KeyCode.S))
        //   {
        //       anim.SetBool("Cowered", true);
        //   }


        // if (Input.GetKey(KeyCode.W))
        // {
        //    anim.SetBool("Cowered", false);
        //}

        anim.SetBool("Cowered", Input.GetAxis("Vertical") < 0);

        if (anim.GetBool("Cowered"))
        {
            return;
        }

        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Move(h);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }


        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;


        if (jump)
        {
            anim.SetTrigger("Jump");
            //anim.SetBool("Jump", false);

            //int i = Random.Range(0, jumpClips.Length);
            // AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            rigidbody2D.AddForce(new Vector2(0f, jumpForce));

            jump = false;
        }
    }


    /// <summary>
    /// 移动
    /// </summary>
    private void Move(float h)
    {
        if (h * rigidbody2D.velocity.x < maxSpeed)
            rigidbody2D.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);

        if (h > 0 && !facingRight)
            Flip();

        else if (h < 0 && facingRight)
            Flip();
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
