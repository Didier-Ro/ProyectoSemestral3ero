using UnityEngine;

public class PowerUpLifeTime : MonoBehaviour
{
    [SerializeField] private float _lifeTime = default;
    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
