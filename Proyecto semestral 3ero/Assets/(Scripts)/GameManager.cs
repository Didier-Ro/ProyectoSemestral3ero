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

    [SerializeField] private int _asteroidMinSizeScore = 100;
    [SerializeField] private int _asteroidMiddleSizeScore = 50;
    [SerializeField] private int _asteroidMaxSizeScore = 25;

    [SerializeField] private GameObject _gameOverText = default;
    [SerializeField] private GameObject _playAgainButton = default;
    [SerializeField] private GameObject _returnMenuButton = default;

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
            _score += _asteroidMinSizeScore;
        }
        else if (size <= _size)
        {
            _score += _asteroidMiddleSizeScore;
        }
        else
        {
            _score += _asteroidMaxSizeScore;
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
        _gameOverText.SetActive(true);
        _playAgainButton.SetActive(true);
        _returnMenuButton.SetActive(true);
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
