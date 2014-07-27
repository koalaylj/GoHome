using UnityEngine;
using System.Collections;

public enum State{
    NORMAL=0,
    FAT=1
};

public enum MotionState { 
    MOVE,
    DOWN,
}

public class MotionController : MonoBehaviour {


    private State _state = State.NORMAL;

	// Update is called once per frame
	void Update () {
	
	}

    void OnDoubleTap(TapGesture gesture)
    {
        if (gesture.Selection == this.gameObject)
        {
            //rigidbody2D.AddForce(Vector2.right * 200);
            if (_state == State.NORMAL)
            {
                _state = State.FAT;
            }
            else {
                _state = State.NORMAL;
            }
        }
    }

    void OnSwipe(SwipeGesture gesture)
    {
        //GameObject selection = gesture.StartSelection;  // we use the object the swipe started on, instead of the current one

        //if (selection == swipeObject)

            string log = "Swiped " + gesture.Direction + " with finger " + gesture.Fingers[0] + " (velocity:" + gesture.Velocity + ", distance: " + gesture.Move.magnitude + " )";
           
            //emitter.Emit(gesture.Direction, gesture.Velocity);
            Debug.Log(log);
    }
}
