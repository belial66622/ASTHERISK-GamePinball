using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLauncher : MonoBehaviour
{

    [SerializeField] protected float _force; // besar gaya yang diberikan saat launch




    // hanya dapat membaca input saat bersentuhan dengan bola saja
    protected virtual void OnCollisionStay(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Ball ball))
        {
            ball.GetComponent<Rigidbody>().AddForce(Vector3.forward * _force);
        }

    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Ball ball))
        {

        }
    }

    // baca input



}
