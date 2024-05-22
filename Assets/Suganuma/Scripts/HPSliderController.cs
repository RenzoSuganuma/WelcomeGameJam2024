using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HPスライダーの位置を更新する機能を提供する
/// </summary>
public class HPSliderController
    : MonoBehaviour
{
    [SerializeField, Header("P1スライダー")] Slider _p1Slider;
    [SerializeField, Header("P1")] Transform _p1;
    [SerializeField, Header("P2スライダー")] Slider _p2Slider;
    [SerializeField, Header("P2")] Transform _p2;

    private RectTransform _rectHP1, _rectHP2;

    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.FindAnyObjectByType<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        _rectHP1 = _p1Slider.GetComponent<RectTransform>();
        _rectHP2 = _p2Slider.GetComponent<RectTransform>();

        _p1Slider.maxValue = _p1.GetComponent<HPHandler>().MaxHealth;
        _p2Slider.maxValue = _p2.GetComponent<HPHandler>().MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        var vpos1 = Camera.main.WorldToScreenPoint(_p1.position + Vector3.right * 5f) - (new Vector3(Screen.width / 2, Screen.height / 2));
        var vpos2 = Camera.main.WorldToScreenPoint(_p2.position + Vector3.right * 5f) - (new Vector3(Screen.width / 2, Screen.height / 2));

        _p1Slider.GetComponent<RectTransform>().localPosition = vpos1;
        _p2Slider.GetComponent<RectTransform>().localPosition = vpos2;

        _p1Slider.value = _p1.GetComponent<HPHandler>().CurrentHealth;
        _p2Slider.value = _p2.GetComponent<HPHandler>().CurrentHealth;
    }
}
