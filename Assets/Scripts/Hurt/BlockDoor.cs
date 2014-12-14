using UnityEngine;
using System.Collections;

public class BlockDoor : KHurt
{
    //触发这个拳头的按钮编号，事件源。
    private int _observer;


    [SerializeField]
    private GameObject _door;

    void Start()
    {
        _observer = (int)Properties[0];

        TriggerHurt[] triggers = this.transform.parent.GetComponentsInChildren<TriggerHurt>();

        foreach (var item in triggers)
        {
            if (item.Id == _observer)
            {
                item.SwitchOnEvent += () =>
                {
                    Destroy(_door);
                };
            }
        }
    }
}