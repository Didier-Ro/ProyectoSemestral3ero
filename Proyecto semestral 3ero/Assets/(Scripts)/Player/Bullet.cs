using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 500.0f;

    [SerializeField] private float _lifeTime = 3f;

    private Rigidbody2D _rigidbody2D = default;
    private SpriteRenderer _spriteRenderer = default;
    private CircleCollider2D _circleCollider2D = default;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * _speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidbody2D.velocity = Vector3.zero;
        _spriteRenderer.enabled = false;
        _circleCollider2D.enabled = false;
        Destroy(gameObject, _lifeTime);
    }
}
