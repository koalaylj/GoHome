using UnityEngine;
using System.Collections;

public class PausePresenter : KPresenter {

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
        Debug.Log("OnClickStart....");
        UIManager.Instance.Hide(this);
        Time.timeScale = 1;
    }
	
}
