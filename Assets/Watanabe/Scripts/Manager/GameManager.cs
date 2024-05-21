using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _timeLimit = 5f;
    [SerializeField]
    private float _timer = 0f;
    [SerializeField]
    private GameObject _player1 = default;
    [SerializeField]
    private GameObject _player2 = default;

    //private PlayerHP _player1Health = default;
    //private PlayerHP _player2Health = default;

    private bool _isGameFinish = false;

    private void Start()
    {
        _timer = 0f;
        _isGameFinish = false;

        //_player1Health = _player1.GetComponent<PlayerHP>();
        //_player2Health = _player2.GetComponent<PlayerHP>();
    }

    private void Update()
    {
        if (_isGameFinish) { return; }

        _timer += Time.deltaTime;
        if (GameOverFlag())
        {
            Debug.Log("GameFinish");
            _isGameFinish = true;
        }
    }

    /// <summary> ゲーム終了判定 </summary>
    /// <returns> ゲーム終了条件を満たしたらtrue </returns>
    private bool GameOverFlag()
    {
        return _timer >= _timeLimit;
        //return _timer >= _timeLimit || _player1Health.HP <= 0 || _player2Health.HP <= 0;
    }
}
