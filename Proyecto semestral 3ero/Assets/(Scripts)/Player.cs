using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet _bulletPrefab = default;

    private Rigidbody2D _rigidbody2D = default;

    private bool _thrusting = default;
    private float _turnDirection = default;

    public float _playerSpeed = 1.0f;
    public float _turnSpeed = 1.0f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0;
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }

    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody2D.AddForce(transform.up * _playerSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidbody2D.AddTorque(_turnDirection * _turnSpeed);
        }
    }

    private void shoot()
    {
        Bullet _bullet = Instantiate(_bulletPrefab, this.transform.position, this.transform.rotation);
        _bullet.Project(transform.up);
    }

}
