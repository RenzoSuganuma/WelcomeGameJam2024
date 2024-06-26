using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 発射物のクラス
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField, Header("プロジェクタイルが何かに当たった時のエフェクト")] GameObject _hitEffect;

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

    /// <summary>
    /// P1またはP2が生成したかの情報
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

        if (_instantiator.ToString() == Instantiator.P1.ToString())
        {
            var rot = this.gameObject.transform.rotation;
            rot.z += Quaternion.EulerRotation(0, 0, Time.deltaTime * 4).z;
            this.gameObject.transform.rotation = rot;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Projectile>(out var projectile))
        {
            if (projectile.Maker != _instantiator)
            { GameObject.Destroy(this.gameObject); }
        }

        // プレイヤーに当たった時
        if (!collision.gameObject.CompareTag(_instantiator.ToString()) && collision.TryGetComponent<HPHandler>(out var component))
        {
            var eff = GameObject.Instantiate(_hitEffect);
            eff.transform.position = transform.position;

            GameObject.Destroy(eff, 1);
            GameObject.Destroy(this.gameObject);
        }

        SEType type = SEType.None;

        if (collision.gameObject.CompareTag(Instantiator.P1.ToString())
            && !collision.gameObject.CompareTag(_instantiator.ToString()))    // P1と当たった場合
        {
            type = SEType.P1Damaged;
        }
        else if (collision.gameObject.CompareTag(Instantiator.P2.ToString())
            && !collision.gameObject.CompareTag(_instantiator.ToString()))    // P2と当たった場合
        {
            type = SEType.P2Damaged;
        }

        AudioManager.Instance.PlaySE(type);
    }
}
