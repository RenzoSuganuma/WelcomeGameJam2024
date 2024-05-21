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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
