using UnityEngine;

public class RadialMovement : MonoBehaviour
{
    [SerializeField] private Player _player = default;

    private Rigidbody2D _rigidBody2D;

    [SerializeField] private float _positionXForce = 2f;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.LookAt(_player.transform.position);
    }

    private void FixedUpdate()
    {
        RadialMove();
    }

    private void RadialMove()
    {
        _rigidBody2D.AddForce(transform.right * _positionXForce, ForceMode2D.Impulse);
    }
}
