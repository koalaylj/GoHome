using UnityEngine;
using System.Collections;

public class TestClick : MonoBehaviour
{


    void OnClick(GameObject go)
    {
        Debug.Log("on click:" + go.name);
    }


    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D _hit_info = Physics2D.Raycast(ray.origin, ray.direction, ray.origin.z + 1500, 1 << LayerMask.NameToLayer("Default"));

            Debug.DrawLine(ray.origin, new Vector3(ray.origin.x, ray.origin.y, ray.origin.z + 150), Color.red);


            if ((_hit_info != null) && (_hit_info.collider != null))// && _hit_info.transform.tag == "Trigger"
            {
                Debug.Log("fuck clicked..." + _hit_info.transform.name + "," + _hit_info.transform.tag);
            }

        }
    }
}
