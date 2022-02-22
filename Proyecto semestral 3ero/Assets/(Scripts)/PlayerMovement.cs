using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rgb2d;

    private bool _thrusting;
    private float _turnDirection;

    public float playerSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    private void Awake()
    {
        _rgb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow)){
            _turnDirection = 1.0f;
        }else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turnDirection = -1.0f;
        }else{
            _turnDirection = 0;
        }
    }
    private void FixedUpdate()
    {
        if (_thrusting){
            _rgb2d.AddForce(this.transform.up * this.playerSpeed);
        }

        if (_turnDirection != 0.0f){
            _rgb2d.AddTorque(_turnDirection * this.turnSpeed);
        }
    }
}
