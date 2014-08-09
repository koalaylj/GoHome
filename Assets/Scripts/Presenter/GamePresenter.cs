using UnityEngine;
using System.Collections;
using System;

public class GamePresenter : Presenter {

    [SerializeField]
    private GameObject _pause;

	// Use this for initialization
	void Start () {
       // _pause.gameObject.GetComponent<UIEventListener>().onClick = OnClick;
        UIEventListener.Get(_pause).onClick = OnClick;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick(GameObject sender) {
        UIManager.Instance.Show("Pause");
        Time.timeScale = 0;
    }
}