using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
    public Color from = Color.white;
    public Color to = Color.white;

    private bool _isDraged = false;
    private RaycastHit2D _hit_info;
    private Vector3 _drag_fix_offset = Vector3.zero;
    private Transform _cache_trans;

    private int _collid_count = 0;

    void Start()
    {
        _cache_trans = this.transform;
    }

    void Update()
    {
        KeyLogic();
    }

    void OnClick(GameObject go)
    {
        Debug.Log("on click:" + go.name);
    }

    void OnDrag(GameObject go, Vector2 delta)
    {
        Debug.Log("on drag : " + go.name);
    }

    void OnDrop(GameObject go, Vector2 delta)
    {
        Debug.Log("on drop : " + go.name);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //_collid_count++;
        Debug.Log("collision enter:" + col.gameObject.name + "," + _collid_count);
    }
    void OnCollisionExit2D(Collision2D col)
    {
        // _collid_count--;
        Debug.Log("collision exit:" + col.gameObject.name + "," + _collid_count);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        _collid_count++;
        Debug.Log("trigger enter:" + col.gameObject.name + "," + _collid_count);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        _collid_count--;
        Debug.Log("trigger exit:" + col.gameObject.name + "," + _collid_count);
    }

    /// <summary>
    /// 触摸，鼠标通用
    /// </summary>
    private void KeyLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("button down");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _hit_info = Physics2D.Raycast(ray.origin, ray.direction, ray.origin.z + 150, 1 << LayerMask.NameToLayer("Draggable"));

            this.collider2D.isTrigger = true;

            if ((_hit_info != null) && (_hit_info.collider != null))//gameObject.tag == "draggable"
            {
                _isDraged = true;
                //Debug.DrawLine(ray.origin, new Vector3(ray.origin.x, ray.origin.y, ray.origin.z + 150), Color.red);
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _drag_fix_offset = _cache_trans.position - pos;
            }
            else
            {
                _isDraged = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("button up");
            _drag_fix_offset = Vector3.zero;
            _isDraged = false;

            if (_collid_count <= 0)
            {
                this.collider2D.isTrigger = false;
                _collid_count = 0;
                Debug.Log("valide pos..");
            }


        }
        else if (Input.GetMouseButton(0))
        {
            if (_isDraged)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 currentPostiont = new Vector3(pos.x + _drag_fix_offset.x, pos.y + _drag_fix_offset.y, _cache_trans.position.z);
                _cache_trans.position = currentPostiont;
            }
        }
    }

    //private void Move(Vector2 target)
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    hit = Physics2D.Raycast(ray.origin, ray.direction, ray.origin.z + 150, 1 << LayerMask.NameToLayer("Draggable"));

    //    if ((hit != null) && (null != hit.collider))
    //    {
    //        _isDraged = true;
    //        Debug.DrawLine(ray.origin, new Vector3(ray.origin.x, ray.origin.y, ray.origin.z + 150), Color.red);
    //    }
    //    if (_isDraged)
    //    {
    //        Vector3 currentPostiont = new Vector3(ray.origin.x, ray.origin.y, _trans.position.z);
    //        _trans.position = currentPostiont;
    //    }
    //}

    //private void TouchLogic()
    //{
    //else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
    //{
    //    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
    //    {

    //        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

    //        Vector3 touchPosition = Vector3.one;

    //        touchPosition.Set(touchDeltaPosition.x,
    //                           transform.position.y,
    //                           touchDeltaPosition.y);


    //        // Move object across XY plane
    //        transform.position = Vector3.Lerp(transform.position,
    //                                                touchPosition,
    //                                                Time.deltaTime);
    //    }
    //}
    //}
}
