using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˕��𔭎˂��������񋟂���N���X
/// </summary>
public class ProjectileShooter : MonoBehaviour
{
    [SerializeField, Header("���ˌ��̃I�u�W�F�N�g")] private Transform origin;
    [SerializeField, Header("���a�̔��a")] private float radius;
    [SerializeField, Header("���̓L�[")] private KeyCode inputKey;
    [SerializeField, Header("���˕�")] private GameObject projectile;
    [SerializeField, Header("�U���^�C�v")] private AttackType attackType;
    [SerializeField, Header("�ʏ픭�˕��̑��x")] private float speedNormal;
    [SerializeField, Header("�������˕��̑��x")] private float speedThunder;
    [SerializeField, Header("�������˕��̐����m��")] private float probability;

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
        // ���������l���ݒ肳��Ă��Ȃ����ɓ���̒l�ŏ�����
        if (origin == null) origin = transform;
        radius = radius < 1 ? 2 : radius;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(inputKey))
        {
            _isFired = true;

            _direction = FindDirection(Mathf.Sin(_elapsedTime) + 90f * Mathf.Deg2Rad);
            GameObject proj;
            Projectile projClass;
            proj = GameObject.Instantiate(projectile);
            projClass = proj.GetComponent<Projectile>();

            switch (attackType)
            {
                case AttackType.Projectile:
                    proj.transform.position = origin.position;
                    projClass.GetSetInstantiator = gameObject.tag == "P1" ? Projectile.Instantiator.P1 : Projectile.Instantiator.P2;
                    projClass.Direction = _direction;
                    projClass.Speed = speedNormal;
                    break;
                case AttackType.Thunder:
                    _direction = origin.up;
                    Random.InitState((int)_elapsedTime);
                    var rand = Random.Range(1, 11);
                    proj.transform.position = origin.position;
                    projClass.GetSetInstantiator = gameObject.tag == "P1" ? Projectile.Instantiator.P1 : Projectile.Instantiator.P2;
                    projClass.Direction = _direction;
                    if (rand < probability)
                    {

                        projClass.Speed = speedThunder;
                        Random.InitState((int)_elapsedTime);
                    }
                    else
                    {
                        projClass.Speed = speedNormal;
                    }
                    break;
            }
        }
    }

    // ���˕�������������
    public Vector3 FindDirection(float theta)
    {
        var h = Mathf.Sin(theta) * radius + origin.position.y;
        var w = Mathf.Cos(theta) * radius + origin.position.x;

        var direction = (new Vector3(w, h, 0) - origin.position).normalized;

        Debug.DrawLine(origin.position, direction + origin.position);

        return direction;
    }
}
