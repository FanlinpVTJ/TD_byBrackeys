using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] private GameObject deathEffectPrefab;
    private UnitHealthSystem unitHealthSystem;
    
    private void OnEnable()
    {
        unitHealthSystem = GetComponent<UnitHealthSystem>();
        unitHealthSystem.OnDeath += EffectOnDestroy;
    }
    private void OnDisable()
    {
        unitHealthSystem.OnDeath -= EffectOnDestroy;
    }
    private void EffectOnDestroy(UnitHealthSystem unitHealthSystem)
    {
        GameObject _enemyDeathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(_enemyDeathEffect, 1f);
    }
}
