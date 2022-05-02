using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab = default;

    private Rigidbody2D _rigidbody2D = default;
    private SpriteRenderer _spriteRenderer = default;
    private Animator _animator = default;
    [SerializeField] private AudioSource _rocketSFX = default;
    [SerializeField] private AudioSource _shieldSFX = default;

    private bool _thrusting = default;
    private bool _turboActivate = true;
    private float _turnDirection = default;

    [SerializeField] private float _playerSpeed = 1f;
    [SerializeField] private float _turnSpeed = 1f;
    [SerializeField] private float _timeToReturnAngularDrag = 0.2f;
    [SerializeField] private float _turboPlayer = 1f;
    [SerializeField] private float _turboReloadTime = 5f;
    [SerializeField] private float _turboLockedTime = 0.2f;

    [SerializeField] private float _invulnerabilityTime = 3f;
    [SerializeField] private float _numberOfOpacityStepsPerSecond = 2f;

    [SerializeField] private float _numberOfRounds = 6f;
    [SerializeField] private float _bulletsPerRound = 10f;
    [SerializeField] private float _delayTime = 0.1f;
    [SerializeField] private Transform _firePoint = default;
    [SerializeField] private GameObject _flashShootEffect = default;

    [SerializeField] private GameObject _shield = default;
    [SerializeField] private float _shieldTime = 6f;
    private bool _isShieldActive = false;

    [SerializeField] private GameObject _explotionEffect = default;

    [SerializeField] private AudioClip _shootSFX = default;
    [SerializeField] private AudioClip _hitSFX = default;
    [SerializeField] private AudioClip _powerUpSFX = default;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameRunnig())
        {
            _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
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
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                if (_turboActivate)
                {
                    StartCoroutine(Turbo());
                }
            }
        }
        _shieldSFX.mute = !_isShieldActive;
    }

    private void FixedUpdate()
    {
        _animator.SetBool("Is Moving", _thrusting);
        _rocketSFX.mute = !_thrusting;

        if (_thrusting)
        {
            _rigidbody2D.AddForce(transform.up * _playerSpeed);
        }

        if (_turnDirection != 0.0f)
        {
            _rigidbody2D.angularDrag = 0.0f;
            _rigidbody2D.AddTorque(_turnDirection * _turnSpeed);
        }
        else
        {
            _rigidbody2D.angularDrag = 5f;
        }
    }

    private void Shoot()
    {
        Bullet _bullet = Instantiate(_bulletPrefab, _firePoint.position, transform.rotation);
        _bullet.Project(transform.up);
        GameObject flash = Instantiate(_flashShootEffect, _firePoint.position, transform.rotation);
        AudioManager.Instance.SFXSelection(_shootSFX, 1);
    }

    IEnumerator Turbo()
    {
        _rigidbody2D.AddForce(transform.up * (_playerSpeed + _turboPlayer), ForceMode2D.Force);
        yield return new WaitForSeconds(_turboLockedTime);
        _turboActivate = false;
        yield return new WaitForSeconds(_turboReloadTime);
        _turboActivate = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("BossBullet"))
        {
            _rigidbody2D.velocity = Vector3.zero;
            StartCoroutine(ReduceTorque());
            GameObject explotion = Instantiate(_explotionEffect, transform.position, transform.rotation);
            CamaraManager.Instance.StrongShakeCamara();
            _spriteRenderer.enabled = false;
            AudioManager.Instance.SFXSelection(_hitSFX, 1);

            GameManager.Instance.PlayerDied();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Infinity Power Up"))
        {
            StartCoroutine(InfinityShoot());
            AudioManager.Instance.SFXSelection(_powerUpSFX, 1);
        }

        if (collision.gameObject.CompareTag("Shield Power Up"))
        {
            StartCoroutine(Shield());
            AudioManager.Instance.SFXSelection(_powerUpSFX, 1);
        }
        Destroy(collision.gameObject);
    }

    public void Respawn()
    {
        ResetPlayer();
    }

    private void ResetPlayer()
    {
        transform.position = Vector3.zero;
        _rigidbody2D.angularVelocity = 0;
        BecomeInvulnerable();
    }

    public void BecomeInvulnerable()
    {
        StartCoroutine(InvulnerabilityCoroutine());
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        _spriteRenderer.enabled = true;
        float opacitySteps = _invulnerabilityTime * _numberOfOpacityStepsPerSecond;
        float opacityTime = _invulnerabilityTime / opacitySteps;

        for (int i = 0; i < opacitySteps; i++)
        {
            _spriteRenderer.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(opacityTime);
            _spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(opacityTime);
        }
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    IEnumerator ReduceTorque()
    {
        _rigidbody2D.angularDrag = 50f;
        yield return new WaitForSeconds(_timeToReturnAngularDrag);
        _rigidbody2D.angularDrag = 0.0f;
    }

    IEnumerator InfinityShoot()
    {
        for (int i = 0; i < _numberOfRounds; i++)
        {
            for (int j = 0; j < _bulletsPerRound; j++)
            {
                Bullet bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
                bullet.Project(transform.up);
                AudioManager.Instance.SFXSelection(_shootSFX, 1);
                yield return new WaitForSeconds(_delayTime);
            }
        }
    }

    IEnumerator Shield()
    {
        _shield.SetActive(true);
        _isShieldActive = true;
        _shield.layer = LayerMask.NameToLayer("Shield");
        yield return new WaitForSeconds(_shieldTime);
        _shield.SetActive(false);
        _isShieldActive = false;
        _shield.layer = LayerMask.NameToLayer("Ignore Collisions");
    }
}
