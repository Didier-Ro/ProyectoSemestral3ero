using UnityEngine;

public class BossBullets : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D = default;

    [SerializeField] private float _speed = 100.0f;
    [SerializeField] private float _lifeTime = 10f;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Projectile(Vector2 direction)
    {
        _rigidBody2D.AddForce(direction * _speed);
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
