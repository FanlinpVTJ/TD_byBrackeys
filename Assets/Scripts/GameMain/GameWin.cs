using System;
using TMPro;
using UnityEngine;

public class GameWin : MonoBehaviour
{

    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private GameObject gameWinUI;
    [SerializeField] private TextMeshProUGUI gameWinWavesCountText;

    private void OnEnable()
    {
        waveSpawner.OnAllWavesHaveDone += GameEnd;
    }
    private void OnDisable()
    {
        waveSpawner.OnAllWavesHaveDone -= GameEnd;
    }

    private void Start()
    {
        gameWinUI.SetActive(false);
    }

    public void GameEnd()
    {
        gameWinWavesCountText.text = waveSpawner.TextWaveIndex.ToString();
        gameWinUI.SetActive(true);       
    }
}
