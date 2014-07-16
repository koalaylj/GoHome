using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDoubleTap(TapGesture gesture)
    {
        if (gesture.Selection != null)
        {
            Debug.Log("doubleTap:" + gesture.Selection.name);
        }

        if (gesture.Selection == this.gameObject)
        {
            rigidbody2D.AddForce(Vector2.right * 200);
        }
        //Debug.Log("doubleTap:" + gesture.Selection.name);
        //rigidbody2D.AddForce(Vector2.right * 200);
    }
}
