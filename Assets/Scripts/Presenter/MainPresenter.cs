using UnityEngine;
using System.Collections;
using System;

public class MainPresenter : Presenter {

    [SerializeField]
    private GameObject _load;

	// Use this for initialization
	void Start () {
       // _pause.gameObject.GetComponent<UIEventListener>().onClick = OnClick;
        UIEventListener.Get(_load).onClick = OnClick;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick(GameObject sender) {
        SceneManager.LoadScene(1);
    }
}