using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _powerUps = default;
    private Vector2 _position = default;
    private float MinX = -10f;
    private float MaxX = 10f;
    private float MinY = -6f;
    private float MaxY = 6f;
    private float _spawningTime = 60f; 
    private bool _spawnPowerUP = true;

    void Update()
    {
        if (_spawnPowerUP)
        {
            StartCoroutine(SpawnPowerUp());
        }
    }

    IEnumerator SpawnPowerUp()
    {
        _spawnPowerUP = false;
        yield return new WaitForSeconds(_spawningTime);
        int NumberofPowerUps = Random.Range(0, _powerUps.Length);
        _position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        GameObject powerUp = Instantiate(_powerUps[NumberofPowerUps], _position, Quaternion.identity);
        powerUp.transform.parent = transform;
        _spawnPowerUP = true;
    }
}
