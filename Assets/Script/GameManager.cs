using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action Gameover;
    public event Action<int> AddScore;
    [SerializeField] CoinManager _coinManager;
    [SerializeField] TrapManager _trapManager;
    [SerializeField] Boundary _boundary;
    int _score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _coinManager._addScore += eventsend;
        _trapManager._gameOver += eventsend;
        _boundary._gameOver += eventsend;
    }

    private void OnDisable()
    {
        _coinManager._addScore -= eventsend;
        _trapManager._gameOver += eventsend;
        _boundary._gameOver += eventsend;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void eventsend(int score)
    {
        switch (score) 
        {
            case -1:
                Gameover?.Invoke();
                break;
            case 1:
                _score += score;
                AddScore?.Invoke(_score);
                break;
        }
    }
}
