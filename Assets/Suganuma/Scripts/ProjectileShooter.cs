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
        // ‰½‚à‰Šú’l‚ªİ’è‚³‚ê‚Ä‚¢‚È‚¢‚É“Á’è‚Ì’l‚Å‰Šú‰»
        if (origin == null) origin = transform;
        radius = radius < 1 ? 2 : radius;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _direction = FindDirection(Mathf.Sin(_elapsedTime) + 90f * Mathf.Deg2Rad);
    }

    // ”­Ë•ûŒü‚ğŒŸõ‚·‚é
    public Vector3 FindDirection(float theta)
    {
        var h = Mathf.Sin(theta) * radius;
        var w = Mathf.Cos(theta) * radius;

        var direction = new Vector3(h, w, 0) - origin.position;

        Debug.DrawLine(Vector3.zero, new Vector3(w, h, 0));

        return direction;
    }
}
