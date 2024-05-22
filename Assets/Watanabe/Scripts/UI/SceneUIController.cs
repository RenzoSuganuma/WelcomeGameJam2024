using UnityEngine;
using UnityEngine.UI;

public class SceneUIController : MonoBehaviour
{
    [SubclassSelector]
    [SerializeReference]
    private ISceneUI _sceneUI = default;

    private void Start()
    {
        if (_sceneUI == null) { return; }

        _sceneUI.Initialize();
    }
}

public class TitleSceneUI : ISceneUI
{
    [SerializeField]
    private Button _startButton = default;
    [SerializeField]
    private Image _rulePanel = default;

    public void Initialize()
    {
        //Button.onClick.AddListener : ボタンを押したときに実行する関数を指定できる
        _startButton.onClick.AddListener(() => SceneLoader.FadeLoad(SceneName.InGame));
        _rulePanel.gameObject.SetActive(false);

        AudioManager.Instance.PlayBGM(BGMType.Title);
        Fade.Instance.StartFadeIn();
    }
}

public class InGameUI : ISceneUI
{
    [SerializeField]
    private Text _timerText = default;
    [SerializeField]
    private Slider _p1Slider = default;
    [SerializeField]
    private Slider _p2Slider = default;
    [SerializeField]
    private HPHandler _p1 = default;
    [SerializeField]
    private HPHandler _p2 = default;

    public Slider P1Slider => _p1Slider;
    public Slider P2Slider => _p2Slider;

    public void Initialize()
    {
        _p1Slider.maxValue = _p1.CurrentHealth;
        _p2Slider.maxValue = _p2.CurrentHealth;

        AudioManager.Instance.PlayBGM(BGMType.InGame);
    }
}

public class ResultSceneUI : ISceneUI
{
    [SerializeField]
    private Button _returnTitleButton = default;

    public void Initialize()
    {
        _returnTitleButton?.onClick.AddListener(() => SceneLoader.FadeLoad(SceneName.Title));

        //switch : 単一条件の分岐処理に役立つ
        var bgm = GameManager.Instance.WinningType switch
        {
            WinningType.P1Win => BGMType.P1Win,
            WinningType.P2Win => BGMType.P2Win,
            WinningType.Draw => BGMType.Draw,
            _ => BGMType.Draw
        };
        AudioManager.Instance.PlayBGM(bgm);
    }
}
