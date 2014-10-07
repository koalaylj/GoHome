using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// ui相关工具类
/// </summary>
public class UIUtil
{
    /// <summary>
    /// 向父节点下添加子节点
    /// </summary>
    /// <param name="parent">父节点</param>
    /// <param name="childPrefab">子物体预制</param>
    /// <returns></returns>
    public static GameObject AppendChild(GameObject parent, GameObject childPrefab)
    {
        GameObject child = GameObject.Instantiate(childPrefab) as GameObject;

        if (child != null && parent != null)
        {
            Transform t = child.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            SetLayer(child, parent.layer);
        }

        return child;
    }

    /// <summary>
    /// 从磁盘上加载预制候添加到父节点下
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public static GameObject AppendChild(GameObject parent, string prefabName)
    {
        if (string.IsNullOrEmpty(prefabName))
            return null;

        GameObject child = IOUtil.LoadPrefab(prefabName);
        return AppendChild(parent, child);
    }

    /// <summary>
    /// 删除当前transtrom所有子物体
    /// </summary>
    /// <param name="parent"></param>
    public static void DestoryTransformChild(Transform parent)
    {
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    /// <summary>
    /// 设置层
    /// </summary>
    /// <param name="go"></param>
    /// <param name="layer"></param>
    public static void SetLayer(GameObject go, int layer)
    {
        go.layer = layer;

        Transform t = go.transform;

        for (int i = 0, imax = t.childCount; i < imax; ++i)
        {
            Transform child = t.GetChild(i);
            SetLayer(child.gameObject, layer);
        }
    }
}