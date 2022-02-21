using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool _thrusting = default;
    private float _turnDirection = default;
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow)){
            _turnDirection = 1.0f;
        }else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turnDirection = -1.0f;
        }else{
            _turnDirection = 0;
        }
    }
}
