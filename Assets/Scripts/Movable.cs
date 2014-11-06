using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{

    private bool _isDraged = false;
    private RaycastHit2D _hit_info;
    private Vector3 _drag_fix_offset = Vector3.zero;
    private Transform _cache_trans;


    void Start()
    {
        _cache_trans = this.transform;
    }

    void Update()
    {
        KeyLogic();
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
            
            Debug.DrawLine(ray.origin, new Vector3(ray.origin.x, ray.origin.y, ray.origin.z + 150), Color.red);

            if ((_hit_info != null) && (_hit_info.collider != null))
            {
                _isDraged = true;
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
        }
        else if (Input.GetMouseButton(0))
        {
            if (_isDraged)
            {
                Debug.Log("dragging...");
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
