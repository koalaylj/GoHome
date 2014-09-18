using UnityEngine;
using System.Collections;
using System;

public class MainPresenter : Presenter
{

    [SerializeField]
    private GameObject _load;

    [SerializeField]
    private int _index = 1;

    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }

    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(_load).onClick = OnClick;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick(GameObject sender)
    {
        SceneManager.LoadScene(_index);
    }
}