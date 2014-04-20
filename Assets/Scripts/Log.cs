using UnityEngine;
using System.Collections;

public class Log : MonoBehaviour {

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(this.name + " collide with " + collision.gameObject.name);
    }
}