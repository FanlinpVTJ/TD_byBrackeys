using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Название зачот
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _timeBetweenWave = 10.5f;
    [SerializeField] private bool _spwanerEnable = true;

    [Header("Using in GUI")]
    public int _textWaveIndex = 1;
    public string _textSpawnTime;

    private float _countdown = 2f;
    private int _waveIndex = 1;

    // Приемлемо, но я бы все равно через корутину сделал))
    // тут это не критично и даже немного удобнее с апдейтом таймера
    private void Update()
    {
        if (_countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countdown = _timeBetweenWave;
        }
        _countdown-= Time.deltaTime;
        
        // TODO: во, тут тоже вместо Clamp лучше подошло бы Mathf.Max(0, _countDown)
        _countdown=Mathf.Clamp(_countdown, 0, Mathf.Infinity);
        _textSpawnTime = string.Format("{0:00.00}", _countdown); // зачет)
    }

    private IEnumerator SpawnWave()
    {
        if (_spwanerEnable)
        {
            _textWaveIndex = _waveIndex;
            //var _waveCount = 1+ _waveIndex/2;

            var _waveCount = 1 + _waveIndex/4;
            for (int i = 0; i < _waveCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.3f);
            }
            _waveIndex++;
        }
        yield return null;
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
    }
}
