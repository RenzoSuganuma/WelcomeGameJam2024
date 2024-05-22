using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// プレイヤのHPの保持をするクラス
/// </summary>
public class HPHandler : MonoBehaviour, IDamage
{
    [SerializeField, Header("体力最大値")] private float _maxHealthPoint;
    [SerializeField, Header("現在の体力")] private float _healthPoint;

    [SerializeField]
    private string _character = "P1";

    public float CurrentHealth
    {
        get => _healthPoint;
        private set
        {
            _healthPoint = value;
            if (UIController == null) { return; }

            var ui = (InGameUI)UIController.SceneUI;
            if (_character == "P1")
            {
                var slider = ui.P1Slider;
                slider.value = _healthPoint;
            }
            else if (_character == "P2")
            {
                var slider = ui.P2Slider;
                slider.value = _healthPoint;
            }
        }
    }
    public float MaxHealth => _maxHealthPoint;
    public SceneUIController UIController { get; set; }

    private void Start()
    {
        CurrentHealth = _maxHealthPoint;
        _character = gameObject.tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            if (projectile.Maker.ToString() != gameObject.tag) { ReceiveDamege(1); }
        }
    }

    public void ReceiveDamege(int value)
    {
        Debug.Log("damaged");
        CurrentHealth -= value;
    }
}
