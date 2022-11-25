using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Название зачот
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float timeBetweenWave = 10.5f;
    [SerializeField] private int startWaveCount = 1;
    [SerializeField] private bool spwanerEnable = true;
    

    [Header("Using in GUI")]
    public int textWaveIndex;
    public string textSpawnTime;

    private float countdown = 2f;
    private int waveIndex = 1;

    // Приемлемо, но я бы все равно через корутину сделал))
    // тут это не критично и даже немного удобнее с апдейтом таймера
    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
        }
        countdown-= Time.deltaTime;

        // TODO: во, тут тоже вместо Clamp лучше подошло бы Mathf.Max(0, _countDown)
        countdown = Mathf.Max(0, countdown);
        textSpawnTime = string.Format("{0:00.00}", countdown); // зачет)
    }

    private IEnumerator SpawnWave()
    {
        if (spwanerEnable)
        {
            textWaveIndex = waveIndex;
            var waveCount = startWaveCount + waveIndex;
            for (int i = 0; i < waveCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.3f);
            }
            waveIndex++;
        }
        yield return null;
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        var enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.SetWaypoints(waypoints);
    }
}
