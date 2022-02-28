using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites = default;

    private SpriteRenderer _spriteRenderer = default;
    private Rigidbody2D _rigidbody2D = default;

    public float _size = 1.0f;
    public float _minSize = 0.5f;
    public float _maxSize = 1.5f;

    [SerializeField] private float _speed = 50.0f;
    [SerializeField] private float _lifeTime = 30.0f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = Vector3.one * _size;

        _rigidbody2D.mass = _size;
    }

    public void SetTrayectory(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * _speed);
        Destroy(gameObject, _lifeTime);
    }
}
