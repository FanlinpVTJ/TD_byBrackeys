using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public static bool _gameOver {get; private set;}
    private void Start()
    {
        _gameOver = false;
        Time.timeScale = 1.0f;
    }
    void Update()
    {
        if (_gameOver)
            return;
      
        if (PlayerStats._liveCount <= 0) 
        {
            EndGame();
        }
    }
 
    private void EndGame()
    {
        Time.timeScale = 0f;
        Debug.Log("nen");
        _gameOver = true;
    }
}
