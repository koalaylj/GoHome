using UnityEngine;
using System.Collections;

public class LevelComponent : MonoBehaviour
{

    [SerializeField]
    private GameObject _start;

    [SerializeField]
    private UISprite _background;

    private int _index;

    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;

        }

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
