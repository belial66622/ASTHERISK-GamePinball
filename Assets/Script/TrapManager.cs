using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public Action<int> _gameOver;
    [SerializeField] List<GameObject> _trapPool;
    [SerializeField] GameObject _trapPrefarb;
    [SerializeField] float _yPosition;
    [SerializeField] int _poolLimit;
    [SerializeField] LayerMask obstacle;
    int _pool=0;
    float _radius = 0.5f;
    bool _gamePause = false, _fulllimit = false;
    [SerializeField] int _spawnTimerSpawn,_spawnTimerOn;

    void Start()
    {
        StartCoroutine(ESpawnTrap());

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameOver(int addscrore)
    {
        _gameOver?.Invoke(addscrore);
        if (_fulllimit)
        {
            _pool--;
            StopAllCoroutines();
            StartCoroutine(ESpawnTrap());

            return;
        }
        _pool--;
    }


    void SpawnTrap()
    {
        float xPos = 0f;
        float zPos = 0f;
        do
        {
            xPos = UnityEngine.Random.Range(-5.0f, 4.0f);
            zPos = UnityEngine.Random.Range(-3.7f, 7.0f);
        } while (CheckSpawnPosition(xPos, zPos));


        Vector3 _randomPosition = new Vector3(xPos, _yPosition, zPos);

        GameObject _readyTrap = GetObjectFromPool(_trapPool);
        if (_readyTrap == null)
        {
            _readyTrap = Instantiate(_trapPrefarb, _randomPosition, Quaternion.identity);
            _readyTrap.transform.parent = transform;
            IHit _hitableInterface = _readyTrap.GetComponent<IHit>();
            _hitableInterface.OnHitBall += GameOver;
            _hitableInterface.Disable += DisableTrap;
            _trapPool.Add(_readyTrap);
            _readyTrap.transform.position = _randomPosition;
            _readyTrap.SetActive(true);

            return;
        }
        _readyTrap.transform.position = _randomPosition;
        _readyTrap.SetActive(true);
    }


    private GameObject GetObjectFromPool(List<GameObject> _pool)
    {
        return _pool.Find(go => !go.activeInHierarchy);
    }


    private bool CheckSpawnPosition(float _x, float _z)
    {
        Collider[] colls = Physics.OverlapSphere(new Vector3(_x, _yPosition, _z),
            _radius,obstacle);
        if (colls.Length > 0) return true;
        else return false;

    }

    IEnumerator ESpawnTrap()
    {
        yield return new WaitForSeconds(0.1f);
        while (_pool < _poolLimit)
        {
            yield return new WaitForSeconds(_spawnTimerSpawn);
            SpawnTrap();
            _pool++;
        }

        _fulllimit = true;
    }

    void DisableTrap()
    {
        if (_fulllimit)
        {
            _pool--;
            StopAllCoroutines();
            StartCoroutine(ESpawnTrap());
            return;
        }
        _pool--;

    }
}
