using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject _visual;
    [SerializeField] protected float _maxForce; // besar gaya yang diberikan saat launch
    [SerializeField] private InputPlayer _input;
   [SerializeField] bool _pressed;
    [SerializeField] protected bool _holdLauncher = false;
    public event Action<float>Launch;
    [SerializeField]private float _maxTimeHold;

    private void OnEnable()
    {
        _input.Launchpressed += Hold;
        _input.Launchcancelled +=Release;
    }

    private void OnDisable()
    {
        _input.Launchpressed -= Hold;
        _input.Launchcancelled -= Release;
    }

    // hanya dapat membaca input saat bersentuhan dengan bola saja
    protected virtual void OnCollisionStay(Collision collision)
        {
        if (collision.transform.TryGetComponent(out Ball ball ))
        {
            _holdLauncher = true;
        }

        }

    protected virtual void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Ball ball))
        {
            _holdLauncher = false;
        }
    }

    // baca input
    private void Hold(bool pressed)
        {
        _pressed = pressed;
        if (_holdLauncher == true)
        {
            StartCoroutine(StartHold());

        }
        }


    private void Release(bool pressed)
    {
        _pressed = pressed;

    }

    private IEnumerator StartHold()
    {
        Vector3 visual = _visual.transform.localPosition;
        float force = 0.0f;
        float timeHold = 0.0f;

        while (_pressed)
        {
            // hitung force menggunakan lerp
            force = Mathf.Lerp(0, _maxForce, timeHold / _maxTimeHold);
            _visual.transform.localPosition = Vector3.Lerp(visual, new Vector3 (0,0,-0.91f), timeHold/_maxTimeHold);
            // tunggu step berikutnya dan naikan timer 
            // agar mendapat nilai force yang lebih besar dari sebelumnya
            yield return new WaitForEndOfFrame();
            timeHold += Time.deltaTime;

        }

        // kalau tombol dilepas, maka proses hold selesai
        _visual.transform.localPosition = visual;
       Launch?.Invoke(force);
        _holdLauncher = false;
    }


    protected void ChildEvent(float force)
    {
        Debug.Log(Launch);
        Launch?.Invoke(force);

    }
}
