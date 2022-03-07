using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player _player = default;

    [SerializeField] private int _lives = 3;
    [SerializeField] private float _respawnTime = 3.0f;
    [SerializeField] private float _respawnInvulnerabityTime = 3.0f;

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
       //TODO-Didier-06/03-Add GameOver logig
    }
}
