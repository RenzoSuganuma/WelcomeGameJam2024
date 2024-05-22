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

    public Transform Min { get => _minWidth; set => _minWidth = value; }
    public Transform Max { get => _maxWidth; set => _maxWidth = value; }
    public readonly float MinWidth => _minWidth.position.x;
    public readonly float MaxWidth => _maxWidth.position.x;
    public readonly float Height => _minWidth.position.y;
}

[Serializable]
public class WeatherController
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

    public void Initialize(Transform parent)
    {
        _rainTimer = 0f;
        _rainInterval = .2f;
        _objectPool = new();
        _damageRainRandom ??= new();
        _spawnRangeRandom ??= new();

        if (_rainRange.Min == null) { _rainRange.Min = parent.GetChild(0).transform; }
        if (_rainRange.Max == null) { _rainRange.Max = parent.GetChild(1).transform; }
    }

    public void OnUpdate(float deltaTime)
    {
        _rainTimer += deltaTime;
        if (_rainTimer >= _rainInterval)
        {
            _rainTimer = 0f;
            if (_weatherType == WeatherType.Rainy) { Rainy(); }
            else if (_weatherType == WeatherType.HeavyRain) { HeavyRain(); }
        }
    }

    public void OnDestroy()
    {
        _objectPool = null;
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
        spawnedRain.transform.position = new Vector2(spawnHol, _rainRange.Height);

        if (spawnedRain.TryGetComponent(out Raindrop raindrop)) { raindrop.Initialize(_objectPool); }
    }
}

public enum WeatherType
{
    Rainy,
    HeavyRain,
}
