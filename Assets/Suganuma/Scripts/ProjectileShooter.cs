using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˕��𔭎˂��������񋟂���N���X
/// </summary>
public class ProjectileShooter : MonoBehaviour
{
    [SerializeField, Header("���ˌ��̃I�u�W�F�N�g�y�r�z")] private Transform _origin;
    [SerializeField, Header("���a�̔��a")] private float _radius;
    [SerializeField, Header("�v���W�F�N�^�C�����˃L�[")] private KeyCode _fireKey;
    [SerializeField, Header("���˕�")] private GameObject _projectile;
    [SerializeField, Header("�U���^�C�v")] private AttackType _attackType;
    [SerializeField, Header("�ʏ픭�˕��̑��x")] private float _speedNormal;
    [SerializeField, Header("�������˕��̑��x")] private float _speedThunder;
    [SerializeField, Header("�������˕��̐����m��")] private float _probability;
    [SerializeField, Header("���E�ɘr��U�鑬�x")] private float _wipeSpeed;
    [SerializeField, Header("�U���C���^�[�o��"), Range(.1f, 3f)] private float _attackInterval;

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
        // ���������l���ݒ肳��Ă��Ȃ����ɓ���̒l�ŏ�����
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

        if (Input.GetKeyDown(_fireKey) && _elapsedTimeAfterAttack > _attackInterval) // ���˃L�[���͎�
        {
            _elapsedTimeAfterAttack = 0f;

            GameObject proj;
            Projectile projClass;
            proj = GameObject.Instantiate(_projectile);
            projClass = proj.GetComponent<Projectile>();

            SEType type = SEType.None;

            if (gameObject.CompareTag(Projectile.Instantiator.P1.ToString()))   // P1�������ꍇ
            {
                type = SEType.P1Attack;
            }
            else if (gameObject.CompareTag(Projectile.Instantiator.P2.ToString()))  // P2�������ꍇ
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
