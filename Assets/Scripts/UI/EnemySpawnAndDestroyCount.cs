using TMPro;
using UnityEngine;

public class EnemySpawnAndDestroyCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UnitsLeft;
    [SerializeField] private WaveSpawner waveSpawner;

    private int spawnCount;


    private void Update()
    {
        UnitsLeft.text = spawnCount.ToString();
    }

    private void OnEnable()
    {
        waveSpawner.OnEnemySpawn += PositiveChange;
    }

    private void OnDisable()
    {
        waveSpawner.OnEnemySpawn -= PositiveChange;
    }
    private void PositiveChange()
    {
        spawnCount++;
    }

    public void SetEnemyDeathAction(UnitHealthSystem enemy)
    {
        enemy.OnDeath += NegativeChange;
    }

    private void NegativeChange(UnitHealthSystem enemy)
    {
        spawnCount--;
        enemy.OnDeath -= NegativeChange;
    }
}
