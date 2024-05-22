using UnityEngine;
using UnityEngine.SceneManagement;

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
    private WeatherController _weatherController = new();
    [SerializeField]
    private SceneUIController _uiController = default;

    private HPHandler _player1Health = default;
    private HPHandler _player2Health = default;

    private bool _isGameFinish = false;

    /// <summary> ゲームの終了フラグ </summary>
    protected bool IsGameFinish
    {
        get => _isGameFinish;
        private set
        {
            _isGameFinish = value;
            if (value)
            {
                var p1HP = _player1Health.CurrentHealth;
                var p2HP = _player2Health.CurrentHealth;

                if (p1HP == p2HP) { WinningType = WinningType.Draw; }
                else if (p1HP > p2HP) { WinningType = WinningType.P1Win; }
                else { WinningType = WinningType.P2Win; }

                SceneLoader.FadeLoad(SceneName.Result);
            }
        }
    }
    protected float Timer
    {
        get => _timer;
        private set
        {
            _timer = value;
            if (_uiController == null) { return; }

            var inGameUI = (InGameUI)_uiController.SceneUI;
            inGameUI.TimerText.text = $"Timer\n{value.ToString("F1")}";
        }
    }

    public WinningType WinningType { get; private set; }

    public static GameManager Instance { get; private set; }

    //Unityの関数の実行順序
    //Awake -> Start -> Update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded; ;
        }
        else { Destroy(gameObject); }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "GameScene")
        {
            InitializeGame();
        }
    }

    private void Start()
    {
        //InitializeGame();
    }

    private void InitializeGame()
    {
        Timer = _timeLimit;
        IsGameFinish = false;
        _weatherController.Initialize(transform);

        if (_player1 == null) { _player1 = GameObject.Find("Shooter_P1"); }
        if (_player2 == null) { _player2 = GameObject.Find("Shooter_P2"); }

        _player1Health = _player1.GetComponent<HPHandler>();
        _player2Health = _player2.GetComponent<HPHandler>();

        if (_uiController == null)
        {
            _uiController = GameObject.FindAnyObjectByType<SceneUIController>();
        }
        _player1Health.UIController = _uiController;
        _player2Health.UIController = _uiController;

        AudioManager.Instance.PlayBGM(BGMType.InGame);
    }

    private void Update()
    {
        if (IsGameFinish) { return; }

        var deltaTime = Time.deltaTime;

        _weatherController.OnUpdate(deltaTime);
        Timer -= deltaTime;
        if (GameOverFlag())
        {
            Debug.Log("GameFinish");
            IsGameFinish = true;
        }
        else if (_timer <= _heavyRainPhaseTime && _weatherController.WeatherType == WeatherType.Rainy)
        {
            _weatherController.WeatherType = WeatherType.HeavyRain;
        }
    }

    private void OnDestroy()
    {
        _weatherController?.OnDestroy();
    }

    /// <summary> ゲーム終了判定 </summary>
    /// <returns> ゲーム終了条件を満たしたらtrue </returns>
    private bool GameOverFlag()
        => _timer <= 0f || _player1Health.CurrentHealth <= 0 || _player2Health.CurrentHealth <= 0;
}

public enum WinningType
{
    None,
    P1Win,
    P2Win,
    Draw
}
