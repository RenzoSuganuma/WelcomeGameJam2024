using UnityEngine;

/// <summary>
/// プレイヤのHPの保持をするクラス
/// </summary>
public class HPHandler : MonoBehaviour, IDamage
{
    [SerializeField, Header("体力最大値")] private float _maxHealthPoint;
    [SerializeField, Header("現在の体力")] private float _healthPoint;

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
