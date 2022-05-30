using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player _player = default;

    private GameObject _bossPrefab = default;
    [SerializeField] private GameObject _boss = default;
    [SerializeField] private GameObject _explotionBoss = default;
    [SerializeField] private Transform _spawnBoss = default;
    [SerializeField] private int _scoreToAppearBoss = default;
    [SerializeField] private int _bossMaxScore = 1000;
    [SerializeField] private bool _isBossAlive = false;
    [SerializeField] private bool _bossOnField = false;
    [SerializeField] private AudioClip _bossMusic = default;
    [SerializeField] private AudioClip _defaultMusic = default;

    [SerializeField] private Text _scoreText = default;
    [SerializeField] private string _scoreString = "Score: ";
    [SerializeField] private int _highScore = default;

    [SerializeField] private Text _lifeText = default;
    [SerializeField] private string _lifeString = "X";

    [SerializeField] private int _lifes = 3;
    [SerializeField] private int _score = default;

    private float _minSize = 0.5f;
    private float _size = 1.0f;

    [SerializeField] private int _asteroidMinSizeScore = 100;
    [SerializeField] private int _asteroidMiddleSizeScore = 50;
    [SerializeField] private int _asteroidMaxSizeScore = 25;

    [SerializeField] private GameObject _pausePanel = default;
    [SerializeField] private GameObject _gameOverText = default;
    [SerializeField] private GameObject _playAgainButton = default;
    [SerializeField] private GameObject _returnMenuButton = default;
    [SerializeField] private GameObject _continueButton = default;
    [SerializeField] private GameObject _pauseText = default;
    [SerializeField] private GameObject _configurationsButton = default;

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
        _lifeText.text = _lifeString + _lifes.ToString();
    }

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeRunningState();
        }

        if (_bossOnField == false)
        {
            if (_scoreToAppearBoss >= _bossMaxScore)
            {
                _isBossAlive = true;
            }
            if (_isBossAlive)
            {
                AppearBoss();
                _isBossAlive = false;
            }
        }

        if(_score > _highScore)
        {
            PlayerPrefs.SetInt("HighScore", _score);
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

        _scoreToAppearBoss = _score;

        _scoreText.text = _scoreString + _score.ToString();
    }

    private void AppearBoss()
    {
        _bossOnField = true;
        _bossPrefab = Instantiate(_boss, _spawnBoss.transform.position, Quaternion.identity);
        AudioManager.Instance.MusicSelection(_bossMusic);
    }

    public void BossDead()
    {
        GameObject explotion = Instantiate(_explotionBoss, _bossPrefab.transform.position, Quaternion.identity);
        Destroy(_bossPrefab);
        AudioManager.Instance.MusicSelection(_defaultMusic);
        _bossOnField = false;
        _bossMaxScore = _score * 2;
    }
    public void PlayerDied()
    {
        _lifes--;

        _lifeText.text = _lifeString + _lifes.ToString();

        if (_lifes <= 0)
        {
            GameOver();
            _lifeText.text = _lifeString + 0.ToString();
        }
        else
        {
            _player.Respawn();
        }
    }

    private void GameOver()
    {
        _player.GetComponent<CapsuleCollider2D>().enabled = false;
        _player.enabled = false;
        _pausePanel.SetActive(true);
        _gameOverText.SetActive(true);
        _playAgainButton.SetActive(true);
        _configurationsButton.SetActive(true);
        _returnMenuButton.SetActive(true);
        _player.GetComponent<AudioSource>().mute = true;
    }

    public void ChangeRunningState()
    {
        _gameRunning = !_gameRunning;

        if (_gameRunning)
        {
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
            _pauseText.SetActive(false);
            _continueButton.SetActive(false);
            _configurationsButton.SetActive(false);
            _returnMenuButton.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            _pausePanel.SetActive(true);
            _pauseText.SetActive(true);
            _continueButton.SetActive(true);
            _configurationsButton.SetActive(true);
            _returnMenuButton.SetActive(true);
        }
    } 

    public void SetConfigurations()
    {
        _player.SetTurnConfigurations();
        AudioManager.Instance.SetAudioConfigurations();
    }

    public bool IsGameRunnig()
    {
        return _gameRunning;
    }
}
