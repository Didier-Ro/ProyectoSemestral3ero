using UnityEngine;

public class BulletPlayerDetection : MonoBehaviour
{
    [SerializeField] private int _bossLife = 200;
    [SerializeField] private int _playerDamage = 50;

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
        }
    }

    private void LostLife()
    {
        _bossLife -= _playerDamage;
    }
}
