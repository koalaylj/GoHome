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

    [SerializeField]
    private GameObject[] _stars;

    public PlayResult Result
    {
        set
        {
            bool isSuccess = value == PlayResult.FAIL;

            _resultSprite.spriteName = isSuccess ? "result_success" : "result_fail";
            _next.SetActive(isSuccess);

            int stars = (int)value;
            _stars[0].SetActive(stars >= 1);
            _stars[1].SetActive(stars >= 2);
            _stars[2].SetActive(stars >= 3);

        }
    }

    void Start()
    {
        UIEventListener.Get(_next).onClick = (go) =>
        {
            SceneManager.LoadScene(SceneManager.SceneIndex + 1);
        };

        UIEventListener.Get(_replay).onClick = (go) =>
        {
            SceneManager.LoadScene(SceneManager.SceneIndex);
        };

        UIEventListener.Get(_back).onClick = (go) =>
        {
            Application.LoadLevel("Main");
        };
    }
}
