using UnityEngine;

public class Raindrop : MonoBehaviour
{
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
        if (collision.gameObject.TryGetComponent(out IDamage target))
        {
            if (collision.gameObject.CompareTag("P1")) { target.ReceiveDamege(1); }
            _objectPool.RemoveObject(gameObject);
        }
    }
}
