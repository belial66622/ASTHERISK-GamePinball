using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CoinManager : MonoBehaviour
{


    // Start is called before the first frame update
    public event Action<int> _addScore;
    [SerializeField] List<GameObject> _coinPool;
    [SerializeField] GameObject _coinPrefarb;
    [SerializeField] float _yPosition;
    [SerializeField] int _poolLimit;
    [SerializeField] LayerMask obstacle;
    [SerializeField]int _pool;
    float _radius = 0.5f;
    bool _gamePause = false, _fulllimit = false;
    [SerializeField] int _spawnTimerSpawn, _spawnTimerOn;
    private void OnDisable()
    {

    }

    void Start()
    {
        StartCoroutine(ESpawnCoin());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Addscore(int addscrore)
    {

        _addScore?.Invoke(addscrore);
        if (_fulllimit)
        {
            _pool--;

            StopAllCoroutines();
            StartCoroutine(ESpawnCoin());
            return;
        }
        _pool--;
    }


    void SpawnCoin()
    {
        _pool++;
        float xPos = 0f;
        float zPos = 0f;
        do
        {
            xPos = UnityEngine.Random.Range(-5.0f, 4.0f);
            zPos = UnityEngine.Random.Range(-3.7f, 7.0f);
        } while (CheckSpawnPosition(xPos, zPos));


        Vector3 _randomPosition = new Vector3(xPos, _yPosition, zPos);

        GameObject _readyCoin = GetObjectFromPool(_coinPool);
        if (_readyCoin == null)
        {
            _readyCoin = Instantiate(_coinPrefarb, _randomPosition, Quaternion.Euler(90, 0, 0));
            _readyCoin.transform.parent = transform;
            IHit _hitableInterface = _readyCoin.GetComponent<IHit>();
            _hitableInterface.OnHitBall += Addscore;
            _hitableInterface.Disable += DisableCoin;
            _coinPool.Add(_readyCoin);
            _readyCoin.transform.position = _randomPosition;
            _readyCoin.SetActive(true);
            return;
        }
        _readyCoin.transform.position = _randomPosition;
        _readyCoin.SetActive(true);

    }


    private GameObject GetObjectFromPool(List<GameObject> _pool)
    {
        return _pool.Find(obj => !obj.activeInHierarchy);
    }


    private bool CheckSpawnPosition(float _x, float _z)
    {
        Collider[] colls = Physics.OverlapSphere(new Vector3(_x, _yPosition, _z),
                        _radius, obstacle);
        if (colls.Length > 0) return true;
        else return false;

    }

    IEnumerator ESpawnCoin()
    {

        while (_pool < _poolLimit)
        {
            yield return new WaitForSeconds(_spawnTimerSpawn);
            SpawnCoin();


        }

        _fulllimit = true;
        yield break;
    }

    void DisableCoin()
    {

        if (_fulllimit)
        {
             _pool--;
            StopAllCoroutines();
            StartCoroutine(ESpawnCoin());
            return;
        }
        _pool--;

    }
}

