using UnityEngine;
using System.Collections;
using System;

public class Strawberry : MonoBehaviour
{

    private Animator _anim;

    /// <summary>
    /// 过关结果
    /// </summary>
    private PlayResult _result = PlayResult.STAR3;

    /// <summary>
    /// 计时器
    /// </summary>
    private float _tick = 0f;

    /// <summary>
    /// 是否开始计时
    /// </summary>
    private bool _started = false;


    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 开始计时
    /// </summary>
    void Start()
    {
        _started = true;
    }

    /// <summary>
    /// 暂停计时
    /// </summary>
    public void Pause()
    {
        _started = false;
    }

    void Update()
    {
        if (_started)
        {
            //计时 计算过关所用时间 超过最大时间则显示失败界面
            _tick += Time.deltaTime;

            if (_tick > SceneManager.SceneConfig.star1)
            {
                _result = PlayResult.FAIL;
                StartEating();

            }
        }
    }

    /// <summary>
    /// 吃到草莓，显示过关结果。
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _result != PlayResult.FAIL)
        {
            Pause();

            if (_tick <= SceneManager.SceneConfig.star3)
            {
                _result = PlayResult.STAR3;
            }
            else if (_tick <= SceneManager.SceneConfig.star2)
            {
                _result = PlayResult.STAR2;
            }
            else
            {
                _result = PlayResult.STAR1;
            }

            this.gameObject.SetActive(false);

            ShowResult();
        }
    }

    /// <summary>
    /// 开始吃草莓
    /// </summary>
    void StartEating()
    {
        Pause();
        _anim.SetTrigger("eat");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(100, 40, 200, 40), "time:" + (int)_tick + "/" + SceneManager.SceneConfig.star1);
    }

    /// <summary>
    /// 显示过关结果(这个函数还被草莓的anmation最后一帧的时候调用)
    /// </summary>
    void ShowResult()
    {
        Pause();
        Time.timeScale = 0;
        Presenter p = UIManager.Instance.Show("Result");
        p.DataContent = _result;
    }
}
