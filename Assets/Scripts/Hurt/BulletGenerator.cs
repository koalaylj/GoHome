using UnityEngine;
using System.Collections;

/// <summary>
/// 冰锥机关
/// </summary>
public class BulletGenerator : KHurt
{
    [SerializeField]
    private GameObject _bulletPrefab;

    public float Speed { get; set; }

    /// <summary>
    /// 生成子弹
    /// </summary>
    public void GenerateBullet()
    {

        GameObject bullet = GameObject.Instantiate(_bulletPrefab) as GameObject;
        //bullet.transform.parent = this.transform;

        bullet.transform.position = transform.position;
        //bullet.transform.Rotate(Vector3.forward, transform.rotation.eulerAngles.z);
        bullet.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);

        BulletHurt hurt = bullet.GetComponent<BulletHurt>();
        hurt.Speed = Speed;
    }
}