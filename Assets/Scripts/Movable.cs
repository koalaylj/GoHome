using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
    bool isBeDraged = false;
    RaycastHit2D hit;


    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction, ray.origin.z + 150, 1 << LayerMask.NameToLayer("Draggable"));

            if ((hit != null) && (null != hit.collider))
            {
                isBeDraged = true;
                Debug.DrawLine(ray.origin, new Vector3(ray.origin.x, ray.origin.y, ray.origin.z + 150), Color.red);
            }
            if (isBeDraged)
            {
                //Vector3 currentPostiont = Input.mousePosition;需要坐标系转换才能使用鼠标坐标，具体见本篇开头坐标系转换链接  
                Vector3 currentPostiont = new Vector3(ray.origin.x, ray.origin.y, transform.position.z);
                transform.position = currentPostiont;
            }
        }
        else
            isBeDraged = false;
    }
}
