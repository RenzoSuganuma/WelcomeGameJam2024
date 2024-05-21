using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˕��𔭎˂��������񋟂���N���X
/// </summary>
public class ProjectileShooter : MonoBehaviour
{
    [SerializeField, Header("���ˌ��̃I�u�W�F�N�g")] private Transform _origin;
    [SerializeField, Header("���a�̔��a")] private float _radius;
    [SerializeField, Header("���̓L�[")] private KeyCode _inputKey;
    [SerializeField, Header("���˕�")] private GameObject _projectile;
    [SerializeField, Header("�U���^�C�v")] private AttackType _attackType;
    [SerializeField, Header("�ʏ픭�˕��̑��x")] private float _speedNormal;
    [SerializeField, Header("�������˕��̑��x")] private float _speedThunder;
    [SerializeField, Header("�������˕��̐����m��")] private float _probability;

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

    // ���˕�������������
    public Vector3 FindDirection(float theta)
    {
        var h = Mathf.Sin(theta) * _radius + _origin.position.y;
        var w = Mathf.Cos(theta) * _radius + _origin.position.x;

        var direction = (new Vector3(w, h, 0) - _origin.position).normalized;

        Debug.DrawLine(_origin.position, direction + _origin.position);

        return direction;
    }
}
