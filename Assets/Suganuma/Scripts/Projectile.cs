using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 発射物のクラス
/// </summary>
public class Projectile : MonoBehaviour
{
    /// <summary>
    /// 目指している方向
    /// </summary>
    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    /// <summary>
    /// 移動速度
    /// </summary>
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    private float _speed;
    private Vector3 _direction = Vector3.up;
    private Rigidbody2D _rb2d;

    private void Start()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            this.gameObject.AddComponent<Rigidbody2D>();
            _rb2d = GetComponent<Rigidbody2D>();
            _rb2d.gravityScale = 0f;
        }

        _speed= _speed < 1 ? 10f : _speed;
    }

    // Update is called once per frame
    void Update()
    {
        _rb2d.velocity = _direction.normalized * _speed;
    }
}
