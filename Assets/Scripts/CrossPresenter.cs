using UnityEngine;
using System.Collections;

public class CrossPresenter : MonoBehaviour {

	private int _speed = 8;


	void Start () {
	
	}
	

	void Update () {
		transform.Rotate(Vector3.forward * Time.deltaTime * _speed);
	}
}
