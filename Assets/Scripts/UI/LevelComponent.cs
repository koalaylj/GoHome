using UnityEngine;
using System.Collections;
using System;

public class LevelComponent : MonoBehaviour
{

    [SerializeField]
    private GameObject _start;

    [SerializeField]
    private UISprite _background;

    /// <summary>
    /// 点击事件
    /// </summary>
    public Action<int> OnClickAction;

    public int Index { get; set; }

    public string Name { get; set; }

    void Start()
    {
        UIEventListener.Get(_start).onClick = (sender) =>
        {
            if (OnClickAction != null)
            {
                OnClickAction(Index);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
