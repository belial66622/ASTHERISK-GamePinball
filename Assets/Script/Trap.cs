using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, IHit
{

    public event Action<int> OnHitBall;
    public event Action Disable;
    [SerializeField] int _disableTime;
    private void OnEnable()
    {
        StartCoroutine(DisableTime());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Start is called before the first frame update

    public void BallHit()
    {
        OnHitBall?.Invoke(-1);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.TryGetComponent(out Ball ball))
        {
            BallHit();
        }
    }


    IEnumerator DisableTime() 
    { 
        yield return new WaitForSeconds(_disableTime);
        this.gameObject.SetActive(false);
        Disable?.Invoke();

    }
}
