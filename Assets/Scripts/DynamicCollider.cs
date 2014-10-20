using UnityEngine;
using System.Collections;

public class DynamicCollider : MonoBehaviour
{

    [SerializeField]
    private float _scaleX = 1f;

    [SerializeField]
    private float _scaleY = 1f;

    private BoxCollider2D _collider;
    private SpriteRenderer _render;

    // Use this for initialization
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //根据帧动画动态调整碰撞框大小
        _collider.center = _render.sprite.bounds.center;
        _collider.size = new Vector2(_render.sprite.bounds.size.x * _scaleX, _render.sprite.bounds.size.y * _scaleY);
    }
}