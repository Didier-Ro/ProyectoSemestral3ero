using UnityEngine;

public class PowerUpLifeTime : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 10f);
    }
}
