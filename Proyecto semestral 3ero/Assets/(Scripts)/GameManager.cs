using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player _player = default;

    [SerializeField] private Text _scoreText = default;
    [SerializeField] private string _scoreString = "Score: ";

    [SerializeField] private int _lives = 3;
    [SerializeField] private int _score = default;

    private float _minSize = 0.5f;
    private float _size = 1.0f;

    private bool _gameRunning = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeRunningState();
        }
    }

    public void AsteroidDestroyed(float size)
    {
        if (size <= _minSize)
        {
            _score += 100;
        }
        else if (size <= _size)
        {
            _score += 50;
        }
        else
        {
            _score += 25;
        }

        _scoreText.text = _scoreString + _score.ToString();
    }
    public void PlayerDied()
    {
        _lives--;

        if (_lives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        _player.transform.position = Vector3.zero;
        _player.BecomeInvulnerable();
    }

    private void GameOver()
    {
        //TODO-Didier-06/03-Add GameOver Logig
    }

    private void ChangeRunningState()
    {
        _gameRunning = !_gameRunning;

        if (_gameRunning)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    } 

    public bool IsGameRunnig()
    {
        return _gameRunning;
    }
}
