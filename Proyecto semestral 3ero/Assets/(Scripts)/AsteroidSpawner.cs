using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid _asteriodPrefab = default;

    [SerializeField] private float _tratectoryVariance = 15.0f;
    [SerializeField] private float _spawnRate = 2.0f;
    [SerializeField] private float _spawnDistance = 15.0f;
    [SerializeField] private int _spawnAmount = 1;

    private float _minSize = 0.5f;
    private float _maxSize = 1.5f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), _spawnRate, _spawnRate);
    }

    private void Spawn()
    { 
        for(int i = 0; i < _spawnAmount; i++)
        {
            Vector3 _spawnDirection = Random.insideUnitCircle.normalized * _spawnDistance;
            Vector3 _spawnpoint = transform.position + _spawnDirection;

            float variance = Random.Range(-_tratectoryVariance, _tratectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(_asteriodPrefab, _spawnpoint, rotation);
            asteroid.SetSize(Random.Range(_minSize, _maxSize));

            asteroid.SetTrayectory(rotation * -_spawnDirection);
        }
    }
}
