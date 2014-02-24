using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
    bool _isDraged = false;
    RaycastHit2D _hit_info;
    Vector3 _drag_fix_offset = Vector3.zero;
    Transform _cache_trans;


    bool hehe = false;

    void Start()
    {
        _cache_trans = this.transform;
    }

    void Update()
    {
        KeyLogic();
    }

    void OnGUI()
    {
        GUI.Button(new Rect(10, 60, 80, 30), hehe.ToString());
    }

    private void KeyLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            _hit_info = Physics2D.Raycast(ray.origin, ray.direction, ray.origin.z + 150, 1 << LayerMask.NameToLayer("Draggable"));

            if ((_hit_info != null) && (_hit_info.collider != null))
            {
                _isDraged = true;
                //Debug.DrawLine(ray.origin, new Vector3(ray.origin.x, ray.origin.y, ray.origin.z + 150), Color.red);
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _drag_fix_offset = _cache_trans.position - pos;
                this.collider2D.isTrigger = true;
            }
            else
            {
                _isDraged = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _drag_fix_offset = Vector3.zero;
            _isDraged = false;
            this.collider2D.isTrigger = false;
        }
        else if (Input.GetMouseButton(0))
        {
            if (_isDraged)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 currentPostiont = new Vector3(pos.x + _drag_fix_offset.x, pos.y + _drag_fix_offset.y, _cache_trans.position.z);
                _cache_trans.position = currentPostiont;

                //if (Physics.)
                //{
                //    hehe = false;
                //}
                //else
                //{
                //    hehe = true;
                //}

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
