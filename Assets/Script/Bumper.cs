using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    string _animationHit= "BumperHit"; 
    [SerializeField] private float multiplier;
    // menyimpan variabel bola sebagai referensi untuk pengecekan
    // untuk mengatur warna bumper
    [SerializeField]
    private Color color;
    private Animator animator;
    

    // komponen renderer pada object yang akan diubah
    private Renderer renderer;


    private void Start()
    {
        animator = GetComponent<Animator>();
        // karena material ada pada component Rendered, maka kita ambil renderernya
        renderer = GetComponent<Renderer>();

        // kita akses materialnya dan kita ubah warna nya saat Start
       // renderer.materials[0].color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // memastikan yang menabrak adalah bola
        if (collision.transform.TryGetComponent(out Ball ball))
        {
            ball.ChangeSpeed(multiplier);
            animator.SetTrigger(_animationHit);
            // kita lakukan debug
            //Debug.Log("Kena Bola");
        }
    }


    public void ChangeColor()
    {
        color = new Color(Random.value, Random.value, Random.value);
        renderer.materials[0].color = color;
    }
}
