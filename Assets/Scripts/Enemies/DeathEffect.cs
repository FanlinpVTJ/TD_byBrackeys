using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private Material deathEffectMaterial;

    private void OnDestroy()
    {
        EffectOnDestroy();
    }
    private void EffectOnDestroy()
    {
        GameObject enemyDeathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        var enemyDeathEffectMaterial = enemyDeathEffect.GetComponentInChildren<ParticleSystem>();
        enemyDeathEffectMaterial.GetComponent<Renderer>().material = deathEffectMaterial;
        Destroy(enemyDeathEffect, 1f);
    }
}
