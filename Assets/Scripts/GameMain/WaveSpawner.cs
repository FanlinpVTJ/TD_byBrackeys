using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public event Action OnEnemySpawn;
    public event Action OnAllWavesHaveDone;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Waypoints waypoints;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private EnemySpawnAndDestroyCount enemySpawnAndDestroyCount;
    [SerializeField] private float timeBetweenWave = 2;
    [SerializeField] private bool spwanerEnable = true; //For enable/disable enemy spawn in inspector
    [SerializeField] private WaveContainer[] waveContainer;
    [SerializeField] private EnemySpawnAndDestroyCount EnemyOnBoardCount;
    [SerializeField] private RetryButton retryButton;
    [SerializeField] private MenuButton menuButton;


    public int TextWaveIndex { get { return textWaveIndex; } }
    public string TextSpawnTime { get { return textSpawnTime; } }

    private int textWaveIndex;
    private string textSpawnTime;
    private float countdown = 2f;
    private int waveIndex = 0;
    private bool IsRoundEnd = false;

    private void OnEnable()
    {
        retryButton.OnRetry += SetIsroundEnd;
        menuButton.OnMenu += SetIsroundEnd;
    }
    private void OnDisable()
    {
        retryButton.OnRetry -= SetIsroundEnd;
        menuButton.OnMenu -= SetIsroundEnd;
    }

    private void Update()
    {
        if (EnemyOnBoardCount.SpawnCount > 0 || IsRoundEnd)
        {
            return;
        }
        if (waveIndex == waveContainer.Length && !IsRoundEnd)
        {
            OnAllWavesHaveDone?.Invoke();
            IsRoundEnd = true;
            return;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
            return;
        }
        textSpawnTime = string.Format("{0:00.00}", countdown);
        countdown -= Time.deltaTime;
        countdown = Mathf.Max(0, countdown);
    }

    private IEnumerator SpawnWave()
    {
        if (spwanerEnable)
        {
            WaveContainer wave = waveContainer[waveIndex];
            textWaveIndex = waveIndex + 1;
            for (int i = 0; i < wave.WaveCount; i++)
            {
                SpawnEnemy(wave.EnemyPrefab);
                yield return new WaitForSeconds(wave.Rate);
            }
            waveIndex++;
        }
        yield return null;
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        var enemyHealthSystem = enemy.GetComponent<UnitHealthSystem>();
        playerStats.SetActionToStats(enemyHealthSystem);
        var enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.SetWaypoints(waypoints);
        OnEnemySpawn.Invoke();
        enemySpawnAndDestroyCount.SetEnemyDeathAction(enemyHealthSystem);
    }

    private void SetIsroundEnd(bool IsRoundEnd)
    {
        this.IsRoundEnd = !IsRoundEnd;
        StopAllCoroutines();
    }
}
