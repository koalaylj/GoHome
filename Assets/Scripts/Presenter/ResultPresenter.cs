using UnityEngine;
using System.Collections;

public class ResultPresenter : Presenter
{

    [SerializeField]
    private GameObject _next;

    [SerializeField]
    private GameObject _replay;

    [SerializeField]
    private GameObject _back;

    [SerializeField]
    private UISprite _resultSprite;

    public PlayResult Result
    {

        set
        {
            switch (value)
            {
                case PlayResult.LOSE:
                    _resultSprite.spriteName = "result_bad";
                    break;
                case PlayResult.NORMAL:
                    _resultSprite.spriteName = "result_soso";
                    break;
                case PlayResult.GOOD:
                    _resultSprite.spriteName = "result_good";
                    break;
            }
        }
    }

    void Start()
    {
        UIEventListener.Get(_next).onClick = OnClickNext;
        UIEventListener.Get(_replay).onClick = OnClickReplay;
        UIEventListener.Get(_back).onClick = OnClickBack;
    }

    private void OnClickBack(GameObject go)
    {

    }

    private void OnClickReplay(GameObject go)
    {
        SceneManager.LoadScene(SceneManager.SceneIndex);
    }

    private void OnClickNext(GameObject go)
    {
        SceneManager.LoadScene(SceneManager.SceneIndex + 1);
    }

}
