using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    private bool _gameEnded = false;

    void Update()
    {
        if (_gameEnded)
            return;

        if (PlayerStats._liveCount <= 0) 
        {
            EndGame();
        }
    }
 
    private void EndGame()
    {
        Debug.Log("End");
        _gameEnded=true;
    }
}
