using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public event Action OnGameOver;

    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverWavesCountText;
   
    private void Start()
    {
        gameOverUI.SetActive(false);
    }
    
    public void GameEnd()
    {
        gameOverWavesCountText.text = waveSpawner.textWaveIndex.ToString();
        gameOverUI.SetActive(true);
        OnGameOver.Invoke();
    }
}
