using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _timeLimit = 60f;
    [Tooltip("この値はTimeLimit未満であること")]
    [SerializeField]
    private float _heavyRainPhaseTime = 30f;
    [SerializeField]
    private float _timer = 0f;
    [SerializeField]
    private GameObject _player1 = default;
    [SerializeField]
    private GameObject _player2 = default;
    [SerializeField]
    private WeatherController _weatherController = default;

    private HPHandler _player1Health = default;
    private HPHandler _player2Health = default;

    private bool _isGameFinish = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    private void Start()
    {
        _timer = _timeLimit;
        _isGameFinish = false;

        _player1Health = _player1.GetComponent<HPHandler>();
        _player2Health = _player2.GetComponent<HPHandler>();
    }

    private void Update()
    {
        if (_isGameFinish) { return; }

        _timer -= Time.deltaTime;
        if (GameOverFlag())
        {
            Debug.Log("GameFinish");
            _isGameFinish = true;
        }
        else if (_timer <= _heavyRainPhaseTime && _weatherController.WeatherType == WeatherType.Rainy)
        {
            _weatherController.WeatherType = WeatherType.HeavyRain;
        }
    }

    /// <summary> ゲーム終了判定 </summary>
    /// <returns> ゲーム終了条件を満たしたらtrue </returns>
    private bool GameOverFlag()
        => _timer >= _timeLimit || _player1Health.CurrentHealth <= 0 || _player2Health.CurrentHealth <= 0;
}
