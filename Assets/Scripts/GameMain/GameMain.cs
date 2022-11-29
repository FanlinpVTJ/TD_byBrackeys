using System.Collections;
using UnityEngine;


public class GameMain : MonoBehaviour
{
    public bool IsGameEnded {get; private set;}
    private GameOver gameOver;
    private PlayerStats playerStats;
   
    private void Awake()
    {
        IsGameEnded = false;
        gameOver = GetComponent<GameOver>();
        playerStats = GetComponent<PlayerStats>();
    }
    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return new WaitUntil(() => playerStats.LiveCount <= 0);        
        EndGame();
    }
 
    private void EndGame()
    {
        gameOver.GameEnd();
        IsGameEnded = true;
    }
}
