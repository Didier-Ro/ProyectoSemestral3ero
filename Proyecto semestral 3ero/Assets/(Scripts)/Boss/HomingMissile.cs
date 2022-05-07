using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D = default;

    [SerializeField] private Transform target = default;
    [SerializeField] private GameObject _explosionEffect = default;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotateSpeed = 200f;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - _rigidBody2D.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        _rigidBody2D.angularVelocity = -rotateAmount * _rotateSpeed;

        _rigidBody2D.velocity = transform.up * _speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject explotion = Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
