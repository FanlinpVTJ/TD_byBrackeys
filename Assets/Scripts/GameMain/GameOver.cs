using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] TextMeshProUGUI _gameOverText;
    private void Start()
    {
        _gameOverUI.SetActive(false);
    }
    private void Update()
    {
        if (GameMain._gameOver)
        {
            _gameOverText.text = waveSpawner._textWaveIndex.ToString();
            _gameOverUI.SetActive(true);
        } 
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {

    }
}
