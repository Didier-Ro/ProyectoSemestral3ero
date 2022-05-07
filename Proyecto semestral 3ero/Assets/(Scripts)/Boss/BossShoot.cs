using System.Collections;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private BossBullets _boosBulletsPrefab = default;
    [SerializeField] private HomingMissile _missilePrefab = default;

    [SerializeField] private Transform _playerPosition = default;

    [SerializeField] private float _delayShootTime = 2f;
    [SerializeField] private bool _passedTime = true;
    [SerializeField] private int _numberOfBullets = 4; 
    [SerializeField] private int _numberOfMissiles = 4;
    [SerializeField] private float _delayMissileTime = 1f;
    [SerializeField] private float _waitingTime = 10f;

    [SerializeField] private AudioClip _bossShootSFX = default;

    void Update()
    {
        _playerPosition = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
        if (_passedTime)
        {
            StartCoroutine(BossShooting());
        }
    }

    IEnumerator BossShooting()
    {
        _passedTime = false;
        for (int i = 0; i < _numberOfBullets; i++)
        {
            yield return new WaitForSeconds(_delayShootTime);
            BossBullets _bullets = Instantiate(_boosBulletsPrefab, transform.position, transform.rotation);
            AudioManager.Instance.SFXSelection(_bossShootSFX, 1);
            Vector2 direction = _playerPosition.transform.position - transform.position;
            _bullets.Projectile(direction);
        }
        StartCoroutine(Missiles());
        yield return new WaitForSeconds(_waitingTime);
        _passedTime = true;
    }

    IEnumerator Missiles()
    {
        for (int i = 0; i < _numberOfMissiles; i++)
        {
            HomingMissile missile = Instantiate(_missilePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_delayMissileTime);
        }
    }
}
