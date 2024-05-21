using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private float radius;

    private float _elapsedTime = 0;
    Vector3 _direction = Vector3.zero;

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
    }

    // 発射方向を検索する
    public Vector3 FindDirection(float theta)
    {
        var h = Mathf.Sin(theta) * radius;
        var w = Mathf.Cos(theta) * radius;

        var direction = new Vector3(h, w, 0) - origin.position;

        Debug.DrawLine(Vector3.zero, new Vector3(w, h, 0));

        return direction;
    }
}
