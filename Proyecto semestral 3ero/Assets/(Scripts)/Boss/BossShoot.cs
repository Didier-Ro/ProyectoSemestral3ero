using System.Collections;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    [SerializeField] private BossBullets _boosBulletsPrefab = default;

    [SerializeField] private Transform _playerPosition = default;

    [SerializeField] private float _delayShootTime = 2f;
    [SerializeField] private bool _passedTime = true;

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
        yield return new WaitForSeconds(_delayShootTime);
        BossBullets _bullets = Instantiate(_boosBulletsPrefab, transform.position, transform.rotation);
        AudioManager.Instance.SFXSelection(_bossShootSFX, 1);
        Vector2 direction = _playerPosition.transform.position - transform.position;
        _bullets.Projectile(direction);
        _passedTime = true;
    }
}
