using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 発射物を発射する方向を提供するクラス
/// </summary>
public class ProjectileShooter : MonoBehaviour
{
    [SerializeField, Header("発射元のオブジェクト【腕】")] private Transform _origin;
    [SerializeField, Header("動径の半径")] private float _radius;
    [SerializeField, Header("プロジェクタイル発射キー")] private KeyCode _fireKey;
    [SerializeField, Header("発射物")] private GameObject _projectile;
    [SerializeField, Header("攻撃タイプ")] private AttackType _attackType;
    [SerializeField, Header("通常発射物の速度")] private float _speedNormal;
    [SerializeField, Header("即着発射物の速度")] private float _speedThunder;
    [SerializeField, Header("即着発射物の生成確率")] private float _probability;
    [SerializeField, Header("左右に腕を振る速度")] private float _wipeSpeed;
    [SerializeField, Header("攻撃インターバル"), Range(.1f, 3f)] private float _attackInterval;

    private float _elapsedTimeAfterAttack = 0f;

    public enum AttackType
    {
        Projectile,
        Thunder
    }

    private float _elapsedGameTime = 0;
    private Vector3 _direction = Vector3.zero;

    private void Start()
    {
        // 何も初期値が設定されていない時に特定の値で初期化
        if (_origin == null) _origin = transform;
        _radius = _radius < 1 ? 2 : _radius;

        _elapsedTimeAfterAttack = _attackInterval;
    }

    private void Update()
    {
        _elapsedGameTime += Time.deltaTime * _wipeSpeed;
        _direction = FindDirection(Mathf.Sin(_elapsedGameTime) + 90f * Mathf.Deg2Rad);
        if (gameObject.CompareTag("P1"))
        { _origin.up = _direction; }

        _elapsedTimeAfterAttack += Time.deltaTime;

        if (Input.GetKeyDown(_fireKey) && _elapsedTimeAfterAttack > _attackInterval) // 発射キー入力時
        {
            _elapsedTimeAfterAttack = 0f;

            GameObject proj;
            Projectile projClass;
            proj = GameObject.Instantiate(_projectile);
            projClass = proj.GetComponent<Projectile>();

            SEType type = SEType.None;

            if (gameObject.CompareTag(Projectile.Instantiator.P1.ToString()))   // P1だった場合
            {
                type = SEType.P1Attack;
            }
            else if (gameObject.CompareTag(Projectile.Instantiator.P2.ToString()))  // P2だった場合
            {
                type = SEType.P2Attack;
            }

            AudioManager.Instance.PlaySE(type);

            switch (_attackType)
            {
                case AttackType.Projectile:
                    proj.transform.position = _origin.position;

                    projClass.Maker = gameObject.CompareTag(Projectile.Instantiator.P1.ToString()) ? Projectile.Instantiator.P1 : Projectile.Instantiator.P2;
                    projClass.Direction = _direction;

                    projClass.Speed = _speedNormal;
                    break;
                case AttackType.Thunder:
                    _direction = _origin.up;
                    Random.InitState(Random.Range(0,128));
                    var rand = Random.Range(1, 11);
                    proj.transform.position = _origin.position;

                    projClass.Maker = gameObject.CompareTag(Projectile.Instantiator.P1.ToString()) ? Projectile.Instantiator.P1 : Projectile.Instantiator.P2;
                    projClass.Direction = _direction;

                    if (rand < _probability)
                    {
                        projClass.Speed = _speedThunder;
                        proj.GetComponent<SpriteRenderer>().color = Color.red;
                        Random.InitState(Random.seed);
                    }
                    else
                    {
                        projClass.Speed = _speedNormal;
                    }
                    break;
            }
        }
    }

    // 発射方向を検索する
    public Vector3 FindDirection(float theta)
    {
        var h = Mathf.Sin(theta) * _radius + _origin.position.y;
        var w = Mathf.Cos(theta) * _radius + _origin.position.x;

        var direction = (new Vector3(w, h, 0) - _origin.position).normalized;

        Debug.DrawLine(_origin.position, direction + _origin.position);

        return direction;
    }
}
