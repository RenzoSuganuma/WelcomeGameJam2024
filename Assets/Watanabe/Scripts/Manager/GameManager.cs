using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _timeLimit = 5f;
    [SerializeField]
    private float _timer = 0f;

    private bool _isGameFinish = false;

    private void Start()
    {
        _timer = 0f;
        _isGameFinish = false;
    }

    private void Update()
    {
        if (_isGameFinish) { return; }

        _timer += Time.deltaTime;
        if (_timer >= _timeLimit)
        {
            Debug.Log("GameFinish");
            _isGameFinish = true;
        }
    }
}
