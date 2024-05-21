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
}
