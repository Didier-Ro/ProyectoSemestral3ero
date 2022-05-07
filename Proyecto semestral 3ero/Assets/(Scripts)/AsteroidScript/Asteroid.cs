using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites = default;

    private SpriteRenderer _spriteRenderer = default;
    private Rigidbody2D _rigidbody2D = default;

    private float _size = 0.8f;
    private float _minSize = 0.2f;
    private float _maxSize = 1f;

    [SerializeField] private float _initialMass = 2f; 

    [SerializeField] private float _speed = 50.0f;
    [SerializeField] private float _lifeTime = 45.0f;
    private float _turnDirection = default;
    [SerializeField] private float _turnSpeed = 10f;

    [SerializeField] private float _minSizeToSplit = 0.2f;

    [SerializeField] private GameObject _explotionEffect = default;
    [SerializeField] private AudioClip _explotionSFX = default;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _rigidbody2D.angularDrag = 1f;
        _turnDirection = Random.Range(-1, 1);
    }

    private void Update()
    {
        _rigidbody2D.AddTorque(_turnDirection * _turnSpeed * Time.deltaTime);
    }
    public void Initialize(bool randomizeSize)
    {
        if (randomizeSize)
        {
            _size = Random.Range(_minSize, _maxSize);
        }

        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * _size;

        _rigidbody2D.mass = _initialMass;
    }

    public void SetTrayectory(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * _speed);
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            GameManager.Instance.AsteroidDestroyed(_size);
            if(_size >= _minSizeToSplit)
            {
                CreateSplit();
                CreateSplit();
            }
            Destroy(gameObject);
            GameObject explotion = Instantiate(_explotionEffect, transform.position, transform.rotation);
            CamaraManager.Instance.SlowShakeCamera();
            AudioManager.Instance.SFXSelection(_explotionSFX, 1);
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
            GameObject explotion = Instantiate(_explotionEffect, transform.position, transform.rotation);
            AudioManager.Instance.SFXSelection(_explotionSFX, 1);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half._size = _size * 0.5f;
        half.Initialize(false);
        half.SetTrayectory(Random.insideUnitCircle.normalized * _speed);
    }
}
