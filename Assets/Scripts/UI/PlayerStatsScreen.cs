using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsScreen : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private TextMeshProUGUI currentPlayerMoney;
    [SerializeField] private TextMeshProUGUI currentPlayerLives;
    
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] private TextMeshProUGUI wavesIndexText;
    [SerializeField] private TextMeshProUGUI wavesSpawnTime;
    

    private void Update()
    {
        wavesIndexText.text = waveSpawner.TextWaveIndex.ToString();
        wavesSpawnTime.text = waveSpawner.TextSpawnTime;
        currentPlayerMoney.text = "$" + playerStats.PlayerMoney.ToString();
        currentPlayerLives.text = playerStats.LiveCount.ToString();
    }
}
