using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _score.text = "Score : ";
    }

    private void OnEnable()
    {
        _gameManager.AddScore += converttostring;    
    }

    private void OnDisable()
    {
        _gameManager.AddScore-= converttostring;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void converttostring(int score)
    {
        _score.text = string.Format("Score : {0}", score.ToString());


    }
}
