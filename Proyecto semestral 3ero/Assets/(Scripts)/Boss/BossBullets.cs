using UnityEngine;

public class BossBullets : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D = default;

    [SerializeField] private float _speed = 100.0f;
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private GameObject _explosionParticle = default;
    private SpriteRenderer _spriteRenderer = default;
    private CircleCollider2D _circleCollider2D = default;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Projectile(Vector2 direction)
    {
        _rigidBody2D.AddForce(direction * _speed);
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _circleCollider2D.enabled = false;
        GameObject explosion = Instantiate(_explosionParticle, transform.position, transform.rotation);;
        _rigidBody2D.velocity = Vector2.zero;
        _rigidBody2D.angularVelocity = 0;
    }
}
