using UnityEngine;

/// <summary>
/// �v���C����HP�̕ێ�������N���X
/// </summary>
public class HPHandler : MonoBehaviour, IDamage
{
    [SerializeField, Header("�̗͍ő�l")] private float _maxHealthPoint;
    [SerializeField, Header("���݂̗̑�")] private float _healthPoint;

    public float CurrentHealth => _healthPoint;


    private void Start()
    {
        _healthPoint = _maxHealthPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            if (projectile.Maker.ToString() != gameObject.tag) { ReceiveDamege(1); }
        }
    }

    public void ReceiveDamege(int value)
    {
        _healthPoint -= value;
    }
}
