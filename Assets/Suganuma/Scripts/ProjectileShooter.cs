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

    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        FindDirection(Mathf.Sin(_elapsedTime) + 90f * Mathf.Deg2Rad, out _direction);
    }

    // ”­Ë•ûŒü‚ğŒŸõ‚·‚é
    public void FindDirection(float theta, out Vector3 direction)
    {
        var h = Mathf.Sin(theta) * radius;
        var w = Mathf.Cos(theta) * radius;

        direction = new Vector3(h, w, 0) - origin.position;

        Debug.DrawLine(Vector3.zero, new Vector3(w, h, 0));
    }
}
