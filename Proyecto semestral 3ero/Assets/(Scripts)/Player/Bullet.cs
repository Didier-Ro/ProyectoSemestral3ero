using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 500.0f;

    [SerializeField] private float _lifeTime = 10.0f;

    private Rigidbody2D _rigidbody2D = default;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * _speed);
        Destroy(gameObject, _lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
