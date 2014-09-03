﻿using UnityEngine;
using System.Collections;

public class PausePresenter : Presenter {

    [SerializeField]
    private GameObject _start;

    [SerializeField]
    private GameObject _replay;

    [SerializeField]
    private GameObject _back2Main;

    void Start()
    {
        UIEventListener.Get(_start).onClick = OnClickStart;
        UIEventListener.Get(_replay).onClick = OnClickReplay;
        UIEventListener.Get(_back2Main).onClick = OnClickBack;
    }

    private void OnClickBack(GameObject go)
    {

    }

    private void OnClickReplay(GameObject go)
    {

    }

    private void OnClickStart(GameObject go)
    {
        UIManager.Instance.Hide(this);
        Time.timeScale = 1;
    }
	
}
