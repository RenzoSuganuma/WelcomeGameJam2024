using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˕��̃N���X
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField, Header("�v���W�F�N�^�C���������ɓ����������̃G�t�F�N�g")] GameObject _hitEffect;

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
    public Instantiator Maker
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
        if (collision.TryGetComponent<Projectile>(out var projectile))
        {
            if (projectile.Maker != _instantiator)
            { GameObject.Destroy(this.gameObject); }
        }

        if (!collision.gameObject.CompareTag(_instantiator.ToString()) && collision.TryGetComponent<HPHandler>(out var component))
        {
            var eff = GameObject.Instantiate(_hitEffect);
            eff.transform.position = transform.position;
            GameObject.Destroy(eff, 1);
            GameObject.Destroy(this.gameObject);
        }
    }
}
