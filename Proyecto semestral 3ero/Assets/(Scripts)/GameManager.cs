using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player _player = default;

    [SerializeField] private Text _scoreText = default;
    [SerializeField] private string _scoreString = "Score: ";

    [SerializeField] private int _lives = 3;
    [SerializeField] private float _respawnTime = 3.0f;
    [SerializeField] private float _respawnInvulnerabityTime = 3.0f;
    [SerializeField] private int _score = default;

    private float _minSize = 0.5f;
    private float _size = 1.0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AsteroidDestroyed(float size)
    {
        if(size <= _minSize)
        {
            _score += 100;
        }
        else if(size <= _size)
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

        if(_lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), _respawnTime);
        }
    }

    private void Respawn()
    {
        _player.transform.position = Vector3.zero;
        _player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        _player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), _respawnInvulnerabityTime);
    }

    private void TurnOnCollisions()
    {
        _player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
       //TODO-Didier-06/03-Add GameOver Logig
    }
}
