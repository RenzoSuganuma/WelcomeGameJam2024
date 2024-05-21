using System;
using UnityEngine;
using Random = System.Random;

[Serializable]
public struct RainRange
{
    [SerializeField]
    private float _minWidth;
    [SerializeField]
    private float _maxWidth;

    public readonly float MinWidth => _minWidth;
    public readonly float MaxWidth => _maxWidth;
}

public class WeatherController : MonoBehaviour
{
    [SerializeField]
    private WeatherType _weatherType = WeatherType.Rainy;
    [SerializeField]
    private GameObject _normalRainPrefab = default;
    [SerializeField]
    private GameObject _damageRainPrefab = default;
    [Tooltip("ダメージが入る雨粒が生成される確率")]
    [Range(1, 100)]
    [SerializeField]
    private int _damageRainProbability = 50;
    [Header("雨が降る横幅")]
    [SerializeField]
    private RainRange _rainRange = new();

    private readonly float _rainInterval = 0.2f;
    private float _rainTimer = 0f;

    private ObjectPool _objectPool = default;
    private Random _random = default;

    public WeatherType WeatherType { get => _weatherType; set => _weatherType = value; }

    private void Start()
    {
        _rainTimer = 0f;
        _objectPool ??= new();
        _random ??= new();
    }

    private void Update()
    {
        _rainTimer += Time.deltaTime;
        if (_rainTimer >= _rainInterval)
        {
            _rainTimer = 0f;
            if (_weatherType == WeatherType.Rainy) { Rainy(); }
            else if (_weatherType == WeatherType.HeavyRain) { HeavyRain(); }
        }
    }

    //豪雨
    //見た目の変更 -> 雲が黒っぽくなる
    //                背景変わる、BGM変わる

    //機能
    //雨の当たり判定大きくなる
    //デフォで雨が降ってる
    //豪雨のときは量が増える

    //雷のMuzzleを増やす

    private void Rainy()
    {

    }

    private void HeavyRain()
    {

    }
}

public enum WeatherType
{
    Rainy,
    HeavyRain,
}
