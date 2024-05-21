using System;
using UnityEngine;
using Random = System.Random;

[Serializable]
public struct RainRange
{
    [SerializeField]
    private Transform _minWidth;
    [SerializeField]
    private Transform _maxWidth;

    public readonly float MinWidth => _minWidth.position.x;
    public readonly float MaxWidth => _maxWidth.position.x;
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
    [SerializeField]
    private float _rainInterval = 0.2f;

    private float _rainTimer = 0f;
    private ObjectPool _objectPool = default;
    private Random _damageRainRandom = default;
    private Random _spawnRangeRandom = default;

    public WeatherType WeatherType { get => _weatherType; set => _weatherType = value; }

    private void Start()
    {
        _rainTimer = 0f;
        _objectPool ??= new();
        _damageRainRandom ??= new();
        _spawnRangeRandom ??= new();
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

    private void Rainy() => SpawnRain();

    private void HeavyRain()
    {
        _rainInterval = 0.05f;
        SpawnRain();
    }

    private void SpawnRain()
    {
        //ダメージを与える雨粒の生成
        var damageRandomValue = _damageRainRandom.Next(0, 100);
        var rain =
            damageRandomValue >= _damageRainProbability ?
            _normalRainPrefab : _damageRainPrefab;
        var spawnedRain = _objectPool.SpawnObject(rain);
        //生成位置の調整
        var spawnHol = _spawnRangeRandom.Next((int)_rainRange.MinWidth, (int)_rainRange.MaxWidth);
        spawnedRain.transform.position = new Vector2(spawnHol, transform.position.y);

        if (spawnedRain.TryGetComponent(out Raindrop raindrop)) { raindrop.Initialize(_objectPool); }
    }
}

public enum WeatherType
{
    Rainy,
    HeavyRain,
}
