using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _ball;
    [SerializeField]
    float _maxSpeed;
    
    [SerializeField]bool _bounce;

        private void Start()
    {
        _ball = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y,0,0.8f), transform.position.z);
        if (_ball.velocity.magnitude > _maxSpeed)
        {
            // kalau melebihi buat vector velocity baru dengan besaran max speed
            _ball.velocity = _ball.velocity.normalized * _maxSpeed;

        }

        //PreventVerticalBounce();
    }

    public void ChangeSpeed(float _multiplier)
    {
        
        _ball.velocity *= _multiplier;

    }

    public void PreventVerticalBounce() 
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.3f) && !_bounce)
        {
            transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y, hitInfo.transform.position.y, hitInfo.transform.position.y + 0.8f), transform.position.z);
            Debug.Log("kena");
        }
        else
        {
            Countdown();
        }
    }


    IEnumerator Countdown()
    {
        _bounce = true;

        yield return new WaitForSeconds(2);

        _bounce = false;
    
    }
}
