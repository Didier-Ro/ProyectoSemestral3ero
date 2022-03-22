using UnityEngine;

public class RadialMovement : MonoBehaviour
{
    [SerializeField] private Player _player = default;

    private Rigidbody2D _rigidBody2D;

    [SerializeField] private float _rotatedVelocity = 1f;
    [SerializeField] private float _distancePlayer = default;

    [SerializeField] private float _minDistance = 3.5f;
    [SerializeField] private float _maxDistance = 4.5f;

    [SerializeField] private float _speed = 10f;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.LookAt(_player.transform.position);
        var direction = _player.transform.position - transform.position;
        _distancePlayer = direction.magnitude;

        if (_distancePlayer < _minDistance)
        {
            transform.Translate(-direction * (Time.deltaTime * _speed));
        }

        if (_distancePlayer > _maxDistance)
        {
            transform.Translate(direction * (Time.deltaTime * _speed));
        }
    }

    private void FixedUpdate()
    {
        RadialMove();
    }

    private void RadialMove()
    {
        transform.RotateAround(_player.transform.position, Vector3.forward, _rotatedVelocity);
    }
}