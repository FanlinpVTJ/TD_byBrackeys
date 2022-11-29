using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public event Action OnEnemySpawn;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private EnemySpawnAndDestroyCount enemySpawnAndDestroyCount;
    [SerializeField] private float timeBetweenWave = 10.5f;
    [SerializeField] private int startWaveCount = 1;
    [SerializeField] private float timeBetweenUnitInWave = 0.3f;
    [SerializeField] private bool spwanerEnable = true;

    [Header("Using in GUI")]
    public int textWaveIndex;
    public string textSpawnTime;

    private float countdown = 2f;
    private int waveIndex = 1;

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
        }
        countdown-= Time.deltaTime;
        countdown = Mathf.Max(0, countdown);
        textSpawnTime = string.Format("{0:00.00}", countdown);
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
                yield return new WaitForSeconds(timeBetweenUnitInWave);
            }
            waveIndex++;
        }
        yield return null;
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        var enemyHealthSystem = enemy.GetComponent<UnitHealthSystem>();
        playerStats.SetActionToStats(enemyHealthSystem);
        var enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.SetWaypoints(waypoints);
        OnEnemySpawn.Invoke();
        enemySpawnAndDestroyCount.SetEnemyDeathAction(enemyHealthSystem);
    }
}
