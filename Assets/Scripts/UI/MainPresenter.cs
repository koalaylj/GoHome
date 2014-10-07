using UnityEngine;
using System.Collections;
using System;

public class MainPresenter : KPresenter
{

    [SerializeField]
    private Transform _missionRoot;

    [SerializeField]
    private GameObject _missionPrefab;

    public override void OnShowing()
    {
        base.OnShowing();
        ShowAllMissions();
    }

    private void ShowAllMissions()
    {
        UIUtil.DestoryTransformChild(_missionRoot);

        foreach (var item in GameManager.AllMissions)
        {
            GameObject child = UIUtil.AppendChild(_missionRoot.gameObject, _missionPrefab);
            MissionComponent com = child.GetComponent<MissionComponent>();
            com.DataContent = item;
        }

        RefreshList();
    }


    /// <summary>
    /// 刷新列表
    /// </summary>
    private void RefreshList()
    {
        UIGrid grid = _missionRoot.GetComponent<UIGrid>();
        grid.repositionNow = true;

        //UIScrollView scrollView = _listRoot.parent.GetComponent<UIScrollView>();
        //scrollView.UpdateScrollbars(true);
        //scrollView.ResetPosition();
    }

}