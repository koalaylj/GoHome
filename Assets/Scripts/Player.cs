using UnityEngine;
using System.Collections;
using System;

public enum State
{
    NORMAL = 0,
    FAT = 1,
    DOWN = 2,
};

public class Player : MonoBehaviour
{

    [SerializeField]
    private float moveForce = 10;

    [SerializeField]
    private float maxSpeed = 50;


    private Camera _camera;

    // 朝向
    private bool _facingRight = true;

    //主角是否落地
    //private bool _grounded = false;

    // 用于检测是否落地
    // private Transform groundCheck;

    private Animator _anim;

    private BoxCollider2D _collider;

    // 是否为空的状态
    private State _state = State.NORMAL;

    private Transform _trans;

    void Start()
    {
        //groundCheck = transform.Find("groundCheck");
        _anim = GetComponent<Animator>();
        _trans = this.transform;
        _camera = Camera.main;
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (_state != State.FAT && _trans.rotation != Quaternion.identity)
        {
           // _trans.rotation = Quaternion.identity;
        }
        //_grounded = Physics2D.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Draggable"));
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void NormalMove(float distance)
    {
        if (_state != State.NORMAL)
        {
            return;
        }

        var h = Math.Sign(distance);

        _anim.SetFloat("speed", Math.Abs(distance));

        if (h * rigidbody2D.velocity.x < maxSpeed)
        {
            rigidbody2D.AddForce(Vector2.right * h * moveForce * Time.deltaTime);
        }

        if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed * Time.deltaTime, rigidbody2D.velocity.y);
        }

        if (h > 0 && !_facingRight)
            Flip();

        else if (h < 0 && _facingRight)
            Flip();
    }

    /// <summary>
    /// 立即停止
    /// </summary>
    private void Stop()
    {
        rigidbody2D.velocity = Vector2.zero;
        _anim.SetFloat("speed", 0);
    }


    /// <summary>
    /// 转身
    /// </summary>
    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnDoubleTap(TapGesture gesture)
    {
        Debug.Log("double click: " + gesture.Selection);

        if (gesture.Selection == this.gameObject)
        {
            if (_state == State.NORMAL)
            {
                ChangeState("fat");
            }
            else if (_state == State.FAT)
            {
                ChangeState("normal");
            }
        }
    }

    void OnSwipe(SwipeGesture gesture)
    {
        Debug.Log("swipe:::" + gesture.Direction);

        if (_state == State.FAT)
        {
            var heading = GetSwipeDirectionVector(gesture.Direction);
            rigidbody2D.AddForce(heading * moveForce / 15);
            // transform.rotation = Quaternion.LookRotation(new Vector3(0,0,heading.z));
        }
        else
        {
            if (gesture.Direction == FingerGestures.SwipeDirection.Down)
            {
                ChangeState("down");
            }
            else if (gesture.Direction == FingerGestures.SwipeDirection.Up)
            {
                ChangeState("up");
            }
        }
    }

    /// <summary>
    /// 状态切换
    /// </summary>
    /// <param name="animationName">state transfer animation name</param>
    private void ChangeState(string animationName)
    {

        switch (animationName)
        {
            case "down":
                _state = State.DOWN;
                break;
            case "up":
                _state = State.NORMAL;
                _trans.rotation = Quaternion.identity;
                break;
            case "normal":
                _state = State.NORMAL;
                _collider.size = _collider.size / 2;
                _collider.center = new Vector2(_collider.center.x, _collider.center.y - 1);
                //rigidbody2D.mass = 1;
                rigidbody2D.gravityScale = 1;
                _trans.rotation = Quaternion.identity;
                break;
            case "fat":
                _state = State.FAT;
                //rigidbody2D.mass = 0;
                _collider.size = _collider.size * 2;
                _collider.center = new Vector2(_collider.center.x, _collider.center.y + 1);
                rigidbody2D.gravityScale = 0.003f;
                break;
            default:
                Debug.LogError("no animation name: " + animationName);
                break;
        }

        _anim.SetTrigger(animationName);
    }

    void OnFingerMove(FingerMotionEvent e)
    {
        //if (e.Phase == FingerMotionPhase.Started)
        //{
        //    Debug.Log("Started moving " + e.Finger);
        //}
        //else if (e.Phase == FingerMotionPhase.Updated)
        //{
        //    Debug.Log("Updated moving " + e.Finger);
        //}
        //else
        //{
        //    Debug.Log("Stopped moving " + e.Finger);
        //}
    }


    void OnFingerStationary(FingerMotionEvent e)
    {
       // if (e.Hit.collider!=null && e.Hit.collider.gameObject.layer == LayerMask.GetMask("UI"))
        {
           // Debug.Log("e.Selection:" + e.Selection);
        }
        if (e.Phase == FingerMotionPhase.Started)
        {
            //Debug.Log("*******Started moving " + e.Finger);
        }
        else if (e.Phase == FingerMotionPhase.Updated)
        {
            // var heading = GetSwipeDirectionVector(gesture.Direction);
            float distance = _camera.ScreenToWorldPoint(e.Position).x - _trans.position.x;

            if (Math.Abs(distance) > 0.1f)
            {
                NormalMove(distance);
            }
            else
            {
                Stop();
            }
        }
        else
        {
            Stop();
        }
    }

    private static Vector3 GetSwipeDirectionVector(FingerGestures.SwipeDirection direction)
    {
        Vector3 direct = Vector3.zero;

        switch (direction)
        {
            case FingerGestures.SwipeDirection.Up:
                direct = Vector3.up;
                break;

            case FingerGestures.SwipeDirection.UpperRightDiagonal:
                direct = 0.5f * (Vector3.up + Vector3.right);
                break;

            case FingerGestures.SwipeDirection.Right:
                direct = Vector3.right;
                break;

            case FingerGestures.SwipeDirection.LowerRightDiagonal:
                direct = 0.5f * (Vector3.down + Vector3.right);
                break;

            case FingerGestures.SwipeDirection.Down:
                direct = Vector3.down;
                break;

            case FingerGestures.SwipeDirection.LowerLeftDiagonal:
                direct = 0.5f * (Vector3.down + Vector3.left);
                break;

            case FingerGestures.SwipeDirection.Left:
                direct = Vector3.left;
                break;

            case FingerGestures.SwipeDirection.UpperLeftDiagonal:
                direct = 0.5f * (Vector3.up + Vector3.left);
                break;

            default:
                Debug.LogError("Unhandled swipe direction: " + direction);
                break;
        }

        return direct;
    }
}
