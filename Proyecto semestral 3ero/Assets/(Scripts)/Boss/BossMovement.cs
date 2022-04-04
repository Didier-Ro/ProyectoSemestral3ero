using UnityEngine;
using DG.Tweening;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float _bossPosition = 3.5f;
    [SerializeField] private float _bossPositionMaxlimit = 3.85f;
    [SerializeField] private Transform _rightWall = default;
    [SerializeField] private Transform _leftWall = default;

    [SerializeField] private float _speed = 20f;

    private int _direction = 1;

    private void Start()
    {
        _rightWall = GameObject.FindGameObjectWithTag("Right Wall").GetComponent<Transform>();
        _leftWall = GameObject.FindGameObjectWithTag("Left Wall").GetComponent<Transform>();    
    }
    void Update()
    {
        transform.DOMoveY(_bossPosition, 2f);

        if (transform.position.y <= _bossPositionMaxlimit)
        {
            gameObject.transform.position += Vector3.right * _direction * _speed * Time.deltaTime;

            if (transform.position.x >= _rightWall.transform.position.x)
            {
                _direction = -1;
            }
            if (transform.position.x <= _leftWall.transform.position.x)
            {
                _direction = 1;
            }
        }
    }
}
