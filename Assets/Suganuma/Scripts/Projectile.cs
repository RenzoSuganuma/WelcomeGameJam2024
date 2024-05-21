using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˕��̃N���X
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField, Header("���˕����C���[")] LayerMask layer;

    /// <summary>
    /// �ڎw���Ă������
    /// </summary>
    public Vector3 Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    /// <summary>
    /// �ړ����x
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

    /// <summary>
    /// P1�܂���P2�������������̏��
    /// </summary>
    public Instantiator GetSetInstantiator
    {
        get { return _instantiator; }
        set { _instantiator = value; }
    }

    private float _speed;
    private Vector3 _direction = Vector3.up;
    private Rigidbody2D _rb2d;
    private Instantiator _instantiator;

    public enum Instantiator
    {
        P1,
        P2
    }

    private void Start()
    {
        if (GetComponent<Rigidbody2D>() == null)
        {
            this.gameObject.AddComponent<Rigidbody2D>();
            _rb2d = GetComponent<Rigidbody2D>();
            _rb2d.gravityScale = 0f;
        }

        _rb2d = GetComponent<Rigidbody2D>();

        _speed = _speed < 1 ? 10f : _speed;
    }

    // Update is called once per frame
    void Update()
    {
        _rb2d.velocity = _direction.normalized * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Projectile>() != null)
        {
            if (collision.GetComponent<Projectile>().GetSetInstantiator != _instantiator)
            { GameObject.Destroy(this.gameObject); }
        }

        if (collision.GetComponent<HPHandler>() != null)
        {
            if (collision.gameObject.tag != _instantiator.ToString())
            { GameObject.Destroy(this.gameObject); }
        }
    }
}
