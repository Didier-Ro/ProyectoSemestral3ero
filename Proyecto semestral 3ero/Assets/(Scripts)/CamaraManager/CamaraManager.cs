using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CamaraManager : MonoBehaviour
{
    public static CamaraManager Instance { get; private set; }

    private Tween _shake = default;

    [SerializeField] Camera _camera = default;

    [SerializeField] private float _duration = 1;
    [SerializeField] private float _strength = 8;
    [SerializeField] private int _vibrato = 10;
    [SerializeField] private float _randomness = 100;

    [SerializeField] private float _duration2 = 1;
    [SerializeField] private float _strength2 = 10;
    [SerializeField] private int _vibrato2 = 5;
    [SerializeField] private float _randomness2 = 100;

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

    public void StrongShakeCamara()
    {
        _shake?.Kill();
        _shake = Camera.main.DOShakePosition(_duration, _strength, _vibrato, _randomness);
        StartCoroutine(ReturnPosition());
    }

    public void SlowShakeCamera()
    {
        _shake?.Kill();
        _shake = Camera.main.DOShakePosition(_duration2, _strength2, _vibrato2, _randomness2);
        StartCoroutine(ReturnPosition());
    }

    IEnumerator ReturnPosition()
    {
        yield return new WaitForSeconds(1f);
        _camera.transform.DOMove(new Vector3(0, 0, -10), 1f);
        yield break;
    }
}
