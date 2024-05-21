using UnityEngine;

/// <summary>
/// プレイヤのHPの保持をするクラス
/// </summary>
public class HPHandler : MonoBehaviour, IDamage
{
    [SerializeField] private float _maxHealthPoint;

    public float CurrentHealth => _healthPoint;

    private float _healthPoint;

    private void Start()
    {
        _healthPoint = _maxHealthPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            if (projectile.GetSetInstantiator.ToString() != gameObject.tag) { ReceiveDamege(1); }
        }
    }

    public void ReceiveDamege(int value)
    {
        _healthPoint -= value;
    }
}
