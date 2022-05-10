using UnityEngine;

public class PowerUpLifeTime : MonoBehaviour
{
    [SerializeField] private float _lifeTime = default;
    [SerializeField] private GameObject _destroyEffect = default;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnDestroy()
    {
        GameObject destroy = Instantiate(_destroyEffect, transform.position, Quaternion.identity);
    }
}
