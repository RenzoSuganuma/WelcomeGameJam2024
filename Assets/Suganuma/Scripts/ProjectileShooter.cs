using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 発射物を発射する方向を提供するクラス
/// </summary>
public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private float radius;
    [SerializeField] private KeyCode inputKey;
    [SerializeField] private GameObject projectile;
    [SerializeField] AttackType attackType;

    public enum AttackType
    {

    }

    private float _elapsedTime = 0;
    private Vector3 _direction = Vector3.zero;

    private void Start()
    {
        // 何も初期値が設定されていない時に特定の値で初期化
        if (origin == null) origin = transform;
        radius = radius < 1 ? 2 : radius;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _direction = FindDirection(Mathf.Sin(_elapsedTime) + 90f * Mathf.Deg2Rad);
        Debug.Log($"{_direction.ToString()}");
        // 入力が入ったら
        if (Input.GetKeyDown(inputKey))
        {
            var proj = GameObject.Instantiate(projectile);
            proj.transform.position = origin.position;
            proj.GetComponent<Projectile>().Direction = _direction;
        }
    }

    // 発射方向を検索する
    public Vector3 FindDirection(float theta)
    {
        var h = Mathf.Sin(theta) * radius + origin.position.y;
        var w = Mathf.Cos(theta) * radius + origin.position.x;

        var direction = (new Vector3(w, h, 0) - origin.position).normalized;

        Debug.DrawLine(origin.position, direction + origin.position);

        return direction;
    }
}
