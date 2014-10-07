using UnityEngine;
using System.Collections;
using System;

public class MissionComponent : KComponent
{

    [SerializeField]
    private GameObject _start;

    [SerializeField]
    private GameObject[] _stars;

    [SerializeField]
    private GameObject _lock;

    [SerializeField]
    private UILabel _name;

    private int _index;

    public override object DataContent
    {
        get
        {
            return _index;
        }
        set
        {
            _index = (int)value;
            MissionPrefsModel pref = GameManager.GetMissionPrefsByIndex(_index);
            SceneConfigModel conf = GameManager.GetSceneConfByIndex(_index);

            _lock.SetActive(pref.Lock);
            _name.text = conf.name;

            if (!pref.Lock)
            {
                _stars[0].SetActive(pref.Star >= 1);
                _stars[1].SetActive(pref.Star >= 2);
                _stars[2].SetActive(pref.Star >= 3);
            }
        }
    }

    void Start()
    {
        UIEventListener.Get(_start).onClick = (sender) =>
        {
            MissionPrefsModel pref = GameManager.GetMissionPrefsByIndex(_index);
            if (!pref.Lock)
            {
                SceneManager.LoadScene(_index);
            }
        };
    }
}