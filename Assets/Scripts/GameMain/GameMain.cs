using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Название пойдет, по крайней мере понятное, это ок
public class GameMain : MonoBehaviour
{
    public bool IsGameEnded {get; private set;}
    private GameOver gameOver;
    private PlayerStats playerStats;
    // TODO: аааааааааа жуть какая
    // TODO: private забыл
    private void Awake()
    {
        IsGameEnded = false;
        gameOver = GetComponent<GameOver>();
        playerStats = GetComponent<PlayerStats>();
    }
    private void Start()
    {
        //StartCoroutine(GameLoop());
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
        yield return new WaitWhile(() => playerStats.LiveCount > 0);        
        EndGame();
    }
 
    private void EndGame()
    {
        gameOver.GameEnd();
        IsGameEnded = true; // TODO: пробелы вокруг = ))
    }
}
