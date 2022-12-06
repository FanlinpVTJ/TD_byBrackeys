using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{

    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private GameObject gameWinUI;
    [SerializeField] private TextMeshProUGUI gameWinWavesCountText;
    [SerializeField] private SceneFading sceneFading;

    private void OnEnable()
    {
        waveSpawner.OnAllWavesHaveDone += ShowWinGameScreen;
    }
    private void OnDisable()
    {
        waveSpawner.OnAllWavesHaveDone -= ShowWinGameScreen;
    }

    private void Start()
    {
        gameWinUI.SetActive(false);
    }

    public void ShowWinGameScreen()
    {
        gameWinWavesCountText.text = waveSpawner.TextWaveIndex.ToString();
        gameWinUI.SetActive(true);       
    }

    public void GoToNextScene()
    {
        sceneFading.RunFadeOutTo(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
