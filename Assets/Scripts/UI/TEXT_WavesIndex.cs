using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TODO: И тут то же самое
public class TEXT_WavesIndex : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] private TextMeshProUGUI _wavesIndexText;
    [SerializeField] private TextMeshProUGUI _wavesSpawnTime;

    private void Update()
    {
        _wavesIndexText.text = waveSpawner._textWaveIndex.ToString();
        _wavesSpawnTime.text = waveSpawner._textSpawnTime;
    }
}
