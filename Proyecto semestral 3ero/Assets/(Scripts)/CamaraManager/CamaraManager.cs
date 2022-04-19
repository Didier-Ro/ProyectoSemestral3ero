using UnityEngine;
using DG.Tweening;

public class CamaraManager : MonoBehaviour
{
    public static CamaraManager Instance { get; private set; }

    private Tween _shake = default;

    [SerializeField] private float _duration = 1;
    [SerializeField] private float _strength = 1;
    [SerializeField] private int _vibrato = 8;
    [SerializeField] private float _randomness = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ShakeCamara()
    {
        _shake?.Kill();
        _shake = Camera.main.DOShakePosition(_duration, _strength, _vibrato, _randomness);
    }
}
