using UnityEngine;

public class RadialMovement : MonoBehaviour
{
    [SerializeField] private Player _player = default;

    private Rigidbody2D _rigidBody2D;

    [SerializeField] private float _positionXForce = 1f;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.LookAt(_player.transform.position);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, _player.transform.position, out hit, 50f))
        {
            Debug.DrawRay(transform.position, _player.transform.position, Color.green);
        }
    }

    private void FixedUpdate()
    {
        RadialMove();
    }

    private void RadialMove()
    {
        //transform.position = new Vector2(_positionXForce * Time.fixedDeltaTime, 0);
        //transform.position = new Vector2(0,_positionXForce * Time.fixedDeltaTime);
    }
}
