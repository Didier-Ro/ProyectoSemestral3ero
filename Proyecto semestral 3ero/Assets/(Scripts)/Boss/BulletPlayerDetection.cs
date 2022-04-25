using UnityEngine;

public class BulletPlayerDetection : MonoBehaviour
{
    [SerializeField] private int _bossLife = 200;
    [SerializeField] private int _playerDamage = 50;

    [SerializeField] private GameObject _hit = default;

    private void Update()
    {
        if (_bossLife <= 0)
        {
            GameManager.Instance.BossDead();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            LostLife();
            AudioManager.Instance.AudioSelection(1, 1);
            GameObject impact = Instantiate(_hit, collision.gameObject.transform.position, transform.rotation);
            CamaraManager.Instance.SlowShakeCamera();
        }
    }

    private void LostLife()
    {
        _bossLife -= _playerDamage;
    }
}
