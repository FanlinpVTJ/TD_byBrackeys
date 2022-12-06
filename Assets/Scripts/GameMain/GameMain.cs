using System.Collections;
using UnityEngine;


public class GameMain : MonoBehaviour
{
    public bool IsGameEnded {get; private set;}
    [SerializeField] private GameOver gameOver;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private RetryButton retryButton;
    [SerializeField] private MenuButton menuButton;

    private void OnEnable()
    {
        gameOver.OnGameOver += SetPauseOrGameOverTimeScale;
        retryButton.OnRetry += SetInGameTimeScale;
        menuButton.OnMenu += SetInGameTimeScale;
        pauseMenu.OnPause += DoInGamePause;
    }
    private void OnDisable()
    {
        gameOver.OnGameOver -= SetPauseOrGameOverTimeScale;
        retryButton.OnRetry -= SetInGameTimeScale;
        menuButton.OnMenu -= SetInGameTimeScale;
        pauseMenu.OnPause -= DoInGamePause;
    }
    private void Start()
    {
        IsGameEnded = false;
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

    private void DoInGamePause(bool IsPause)
    {
        if (IsPause)
            SetPauseOrGameOverTimeScale();
        else
            SetInGameTimeScale();
    }

    private void SetPauseOrGameOverTimeScale()
    {
        Time.timeScale = 0.0f;
    }

    private void SetInGameTimeScale()
    {
        Time.timeScale = 1.0f;
    }
}
