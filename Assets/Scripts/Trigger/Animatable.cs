using UnityEngine;
using System.Collections;

public class Animatable : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private bool _animationPlayed = false;

    // Update is called once per frame
    void Update()
    {
        if (_animationPlayed)
        {
            AnimatorStateInfo stateinfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (stateinfo.normalizedTime > 1f)
            {
                Debug.Log("play over...");
                _animationPlayed = false;
                OnEnd();
            }
        }
    }

    public virtual void Activate()
    {
        if (_animationPlayed)
        {
            return;
        }

        _animationPlayed = true;
        _animator.SetTrigger("activate");
    }

    public virtual void OnEnd()
    {

    }

}
