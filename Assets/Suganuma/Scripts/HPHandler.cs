using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤのHPの保持をするクラス
/// </summary>
public class HPHandler : MonoBehaviour
{
    [SerializeField] private float maxHealthPoint;

    public float CurrentHealth => _healthPoint;

    private float _healthPoint;

    private void Start()
    {
        _healthPoint = maxHealthPoint;
    }

    private void Update()
    {
        if(_healthPoint < 1)
        {
            Debug.Log(gameObject.name + "Is Death");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Projectile>() != null)
        {
            if (collision.GetComponent<Projectile>().GetSetInstantiator.ToString() != gameObject.tag)
            { _healthPoint--; }
        }
    }
}
