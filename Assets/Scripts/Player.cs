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

    /// <summary>
    /// 移动速度(动力大小)
    /// </summary>
    [SerializeField]
    private float moveForce = 10;

    /// <summary>
    /// 最大速度
    /// </summary>
    [SerializeField]
    private float maxSpeed = 50;

    /// <summary>
    /// 图片渲染器
    /// </summary>
    private SpriteRenderer _render;

    /// <summary>
    /// 动画控制器
    /// </summary>
    private Animator _anim;

    /// <summary>
    /// 主摄像机
    /// </summary>
    private Camera _camera;

    /// <summary>
    /// 朝向 
    /// </summary>
    private bool _facingRight = true;

    //主角是否落地
    private bool _grounded = true;

    // 用于检测是否落地
    private Transform _groundCheck;

    /// <summary>
    /// 碰撞框
    /// </summary>
    private BoxCollider2D _collider;

    // 是否为空的状态
    private State _state = State.NORMAL;

    private Transform _trans;

    //Vector3 _swipDirection = Vector3.forward;

    void Start()
    {
        _groundCheck = transform.Find("groundCheck");
        _anim = GetComponent<Animator>();
        _trans = this.transform;
        _camera = Camera.main;
        _collider = GetComponent<BoxCollider2D>();
        _render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //根据帧动画动态调整碰撞框大小
        _collider.center = _render.sprite.bounds.center;
        _collider.size = new Vector2(_render.sprite.bounds.size.x * 0.75f, _render.sprite.bounds.size.y);

        if (_state != State.FAT && _trans.rotation != Quaternion.identity)
        {
            _trans.rotation = Quaternion.identity;
        }

        if (_state == State.FAT)
        {

        }

        _grounded = Physics2D.Linecast(_trans.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == Constant.TAG_HURT)
        {
            //TODO 播放死亡动画
            SceneManager.ShowResult(PlayResult.FAIL);
        }
    }


    /// <summary>
    /// 移动
    /// </summary>
    private void NormalMove(float distance)
    {
        if (_state != State.NORMAL || !_grounded)
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
        //Debug.Log("swipe:::" + gesture.Direction + "," + gesture.Move + "," + gesture.Velocity);

        if (_state == State.FAT)
        {
            //var heading = GetSwipeDirectionVector(gesture.Direction);

            var heading = Vector3.Normalize(gesture.Move);
            rigidbody2D.AddForce(heading * moveForce / 15);

            //var heading = Vector3.Normalize(gesture.Move);
            //var direction = Vector3.Lerp(transform.forward, heading, Time.deltaTime * 4);
            //transform.LookAt(direction);
            //rigidbody2D.AddForce(heading * moveForce / 15);

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
                // _collider.size = _collider.size / 2;
                //_collider.center = new Vector2(_collider.center.x, _collider.center.y - 1);
                rigidbody2D.gravityScale = 1;
                _trans.rotation = Quaternion.identity;
                break;
            case "fat":
                _state = State.FAT;
                //_collider.size = _collider.size * 2;
                //_collider.center = new Vector2(_collider.center.x, _collider.center.y + 1);
                rigidbody2D.gravityScale = 0;
                break;
            default:
                Debug.LogError("no animation name: " + animationName);
                break;
        }

        _anim.SetTrigger(animationName);
    }

    void OnFingerStationary(FingerMotionEvent e)
    {
        // if (e.Hit.collider!=null && e.Hit.collider.gameObject.layer == LayerMask.GetMask("UI"))
        //{
        // Debug.Log("e.Selection:" + e.Selection);
        //}
        if (e.Phase == FingerMotionPhase.Started)
        {
            //Debug.Log("*******Started moving " + e.Finger);
        }
        else if (e.Phase == FingerMotionPhase.Updated)
        {
            // var heading = GetSwipeDirectionVector(gesture.Direction);

            //如果点中自己的时候 一般为双击变形 此时不进行位移
            //if (e.Hit.collider != null && e.Hit.collider.gameObject == this.gameObject)
            //{
            //    return;
            //}

            float distance = _camera.ScreenToWorldPoint(e.Position).x - _trans.position.x;

            if (Math.Abs(distance) > 0.1f)
            {
                NormalMove(distance);
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
