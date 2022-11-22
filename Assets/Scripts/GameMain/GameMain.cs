using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Название пойдет, по крайней мере понятное, это ок
public class GameMain : MonoBehaviour
{
    private bool _gameEnded = false;

    // TODO: аааааааааа жуть какая
    // TODO: private забыл
    void Update()
    {
        if (_gameEnded)
            return;

        if (PlayerStats._liveCount <= 0) 
        {
            EndGame();
        }
    }

    // TODO: красивше же?
    // ждать, пока кол-во жизней больше нуля, потом закончить игру
    
    // TODO: вот тут после () => - это лямбда-выражение,
    // оно будет вызываться каждый кадр, чтобы проверять условие
    // по сути это то же самое, что и у тебя в апдейте, только в 1 строчку
    
    // TODO: есть еще WaitUntil, он работает наоборот: ждать, пока жизни не станут <= 0
    // yield return new WaitUntil(() => PlayerStats._liveCount <= 0);
    private IEnumerator GameLoop()
    {
        yield return new WaitWhile(() => PlayerStats._liveCount > 0);
        EndGame();
    }
 
    private void EndGame()
    {
        Debug.Log("End");
        _gameEnded=true; // TODO: пробелы вокруг = ))
    }
}
