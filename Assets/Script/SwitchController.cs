using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Material _offMaterial,_onMaterial;

    // menyimpan state switch apakah nyala atau mati
    //private bool _isOn;



    // komponen renderer pada object yang akan diubah
    private Renderer _renderer;
    // menggantikan isOn
    private ESwitchState _state;


    private void Start()
    {
        // ambil renderernya
        _renderer = GetComponent<Renderer>();

        /*
        // set switch nya mati baik di state, maupun materialnya
        _isOn = false;
        GetComponent<Renderer>().material = _offMaterial;
        // pada fungsi start mulai langsung jalankan timer
        */
        
        Set(false);

        StartCoroutine(BlinkTimerStart(5));
    }

    // menyimpan variabel bola sebagai referensi untuk pengecekan

    private void OnTriggerEnter(Collider other)
    {
        // memastikan yang menabrak adalah bola
        if (other.transform.TryGetComponent<Ball>(out Ball ball))
        {

            Toggle();
            //StartCoroutine(Blink(2));
            //Set(!_isOn);

            // kita lakukan debug
            //Debug.Log("Kena Bola");
        }
    }

    private void Set(bool active)
    {
        if (active == true)
        {
            _state = ESwitchState.On;
            _renderer.material = _onMaterial;

            // hentikan proses blink
            StopAllCoroutines();
        }
        else
        {
            _state = ESwitchState.Off;
            _renderer.material = _offMaterial;
            StartCoroutine(BlinkTimerStart(5));
        }
    }


    private IEnumerator Blink(int times)
    {
       
        // set statenya menjadi blink dulu sebelum mulai proses
        _state = ESwitchState.Blink;

        // mulai proses blink tanpa mengubah state lagi
        for (int i = 0; i < times; i++)
        {
            _renderer.material = _onMaterial;
            yield return new WaitForSeconds(0.1f);
            _renderer.material = _offMaterial;
            yield return new WaitForSeconds(0.1f);
        }

        // set menjadi off kembali setelah proses blink
        _state = ESwitchState.Off;

        StartCoroutine(BlinkTimerStart(5));
    }


    private void Toggle()
    {
        // dari on --> off
        if (_state == ESwitchState.On)
        {
            Set(false);
        }
        // dari off --> on atau blink --> on
        else
        {
            Set(true);
        }
    }

    private IEnumerator BlinkTimerStart(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(Blink(5));
    }
}
