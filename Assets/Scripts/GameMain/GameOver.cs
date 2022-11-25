using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI gameOverWavesCountText;
   
    private void Start()
    {
        gameOverUI.SetActive(false);
    }
    
    public void GameEnd()
    {
        gameOverWavesCountText.text = waveSpawner.textWaveIndex.ToString();
        gameOverUI.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {

    }
}
