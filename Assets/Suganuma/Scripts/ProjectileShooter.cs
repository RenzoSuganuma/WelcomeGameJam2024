using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 発射物を発射する方向を提供するクラス
/// </summary>
public class ProjectileShooter : MonoBehaviour
{
    [SerializeField, Header("発射元のオブジェクト")] private Transform _origin;
    [SerializeField, Header("動径の半径")] private float _radius;
    [SerializeField, Header("入力キー")] private KeyCode _inputKey;
    [SerializeField, Header("発射物")] private GameObject _projectile;
    [SerializeField, Header("攻撃タイプ")] private AttackType _attackType;
    [SerializeField, Header("通常発射物の速度")] private float _speedNormal;
    [SerializeField, Header("即着発射物の速度")] private float _speedThunder;
    [SerializeField, Header("即着発射物の生成確率")] private float _probability;

    bool _isFired = false;

    public enum AttackType
    {
        Projectile,
        Thunder
    }

    private float _elapsedTime = 0;
    private Vector3 _direction = Vector3.zero;

    private void Start()
    {
        // 何も初期値が設定されていない時に特定の値で初期化
        if (_origin == null) _origin = transform;
        _radius = _radius < 1 ? 2 : _radius;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(_inputKey))
        {
            _isFired = true;

            _direction = FindDirection(Mathf.Sin(_elapsedTime) + 90f * Mathf.Deg2Rad);
            GameObject proj;
            Projectile projClass;
            proj = GameObject.Instantiate(_projectile);
            projClass = proj.GetComponent<Projectile>();

            switch (_attackType)
            {
                case AttackType.Projectile:
                    proj.transform.position = _origin.position;
                    projClass.GetSetInstantiator = gameObject.tag == "P1" ? Projectile.Instantiator.P1 : Projectile.Instantiator.P2;
                    projClass.Direction = _direction;
                    projClass.Speed = _speedNormal;
                    break;
                case AttackType.Thunder:
                    _direction = _origin.up;
                    Random.InitState((int)_elapsedTime);
                    var rand = Random.Range(1, 11);
                    proj.transform.position = _origin.position;
                    projClass.GetSetInstantiator = gameObject.tag == "P1" ? Projectile.Instantiator.P1 : Projectile.Instantiator.P2;
                    projClass.Direction = _direction;
                    if (rand < _probability)
                    {

                        projClass.Speed = _speedThunder;
                        Random.InitState((int)_elapsedTime);
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
