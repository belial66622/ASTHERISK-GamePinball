using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _ball;
    [SerializeField]
    float _maxSpeed;

        private void Start()
    {
        _ball = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y,0,0.8f), transform.position.z);
    }

    public void ChangeSpeed(float _multiplier)
    {
        
        _ball.velocity *= _multiplier;
        _ball.velocity = Vector3.ClampMagnitude(_ball.velocity, _maxSpeed);
    }

}
