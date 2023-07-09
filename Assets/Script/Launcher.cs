using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Launcher : MonoBehaviour
{
        [SerializeField] float force; // besar gaya yang diberikan saat launch
    [SerializeField] private InputPlayer _input;
   [SerializeField] bool _pressed;

    private void OnEnable()
    {
        _input.Launchpressed += ReadInput;
        _input.Launchcancelled +=ReadInput;
    }

    private void OnDisable()
    {
        _input.Launchpressed -= ReadInput;
        _input.Launchcancelled -= ReadInput;
    }

    // hanya dapat membaca input saat bersentuhan dengan bola saja
    protected virtual void OnCollisionStay(Collision collision)
        {
        if (collision.transform.TryGetComponent(out Ball ball))
        {
            ball.ChangeSpeed(20);
            //ball.GetComponent<Collider>().GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
            Debug.Log("bisa");
        }

        }


        // baca input
        private void ReadInput(bool pressed)
        {
            _pressed= pressed;
            Debug.Log("press");

    }


}
