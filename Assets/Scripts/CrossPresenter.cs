using UnityEngine;
using System.Collections;

public class CrossPresenter : MonoBehaviour {

	private int _speed = -10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * Time.deltaTime * _speed);
	}
}
