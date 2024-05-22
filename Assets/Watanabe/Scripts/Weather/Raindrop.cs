using UnityEngine;

public class Raindrop : MonoBehaviour
{
    [SerializeField]
    private RaindropType _raindropType = RaindropType.Normal;

    private ObjectPool _objectPool = default;

    public void Initialize(ObjectPool objectPool)
    {
        _objectPool = objectPool;
    }

    private void OnBecameInvisible()
    {
        _objectPool.RemoveObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_raindropType == RaindropType.Normal)
        {
            _objectPool.RemoveObject(gameObject);
            return;
        }

        if (collision.gameObject.TryGetComponent(out IDamage target))
        {
            if (collision.gameObject.CompareTag("P1")) { target.ReceiveDamege(1); }
        }
    }
}

public enum RaindropType
{
    Normal,
    Damage,
}
