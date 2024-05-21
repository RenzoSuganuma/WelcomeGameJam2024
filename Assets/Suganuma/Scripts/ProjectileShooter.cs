using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˕��𔭎˂��������񋟂���N���X
/// </summary>
public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private float radius;
    [SerializeField] private KeyCode inputKey;
    [SerializeField] private GameObject projectile;

    private float _elapsedTime = 0;
    private Vector3 _direction = Vector3.zero;

    private void Start()
    {
        // ���������l���ݒ肳��Ă��Ȃ����ɓ���̒l�ŏ�����
        if (origin == null) origin = transform;
        radius = radius < 1 ? 2 : radius;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _direction = FindDirection(Mathf.Sin(_elapsedTime) + 90f * Mathf.Deg2Rad);

        // ���͂���������
        if (Input.GetKeyDown(inputKey))
        {
            var proj = GameObject.Instantiate(projectile);
            proj.transform.position = origin.position;


        }
    }

    // ���˕�������������
    public Vector3 FindDirection(float theta)
    {
        var h = Mathf.Sin(theta) * radius;
        var w = Mathf.Cos(theta) * radius;

        var direction = new Vector3(h, w, 0) - origin.position;

        Debug.DrawLine(Vector3.zero, new Vector3(w, h, 0));

        return direction;
    }
}
