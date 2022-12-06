using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    
    [SerializeField] private GameOver gameOver;
    [SerializeField] private GameWin gameWin;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private WaveSpawner waveSpawner;

    private void OnEnable()
    {
        pauseMenu.OnPause += DoInGamePause;
        waveSpawner.OnAllWavesHaveDone += SetPlayerPrefs;
    }
    private void OnDisable()
    {
        pauseMenu.OnPause -= DoInGamePause;
        waveSpawner.OnAllWavesHaveDone -= SetPlayerPrefs;
    }
    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return new WaitUntil(() => playerStats.LiveCount <= 0);        
        ShowGameOverScreen();
    }
 
    private void ShowGameOverScreen()
    {
        gameOver.GameEnd();
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

    private void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex);
    }
}
