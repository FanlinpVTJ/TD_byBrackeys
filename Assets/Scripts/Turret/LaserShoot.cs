using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour, IDamageType
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private ParticleSystem laserDamageEffect;
    [SerializeField] private Light laserDamageLightEffect;
    [SerializeField] private float startlaserDamage;
    [SerializeField] private float startlaserSlowingFactor;

    private Transform[] firePointTransform;
    private UnitHealthSystem targetUnitHealthSystem;
    private EnemyMovement targetEnemyMovement;
    private Transform currentTargetTransform;
    private float currentDeltaTimeLaserDamage;
    private float laserDamage;
    private float laserSlowingFactor;

    private void Start()
    {
        laserDamageLightEffect.enabled = false;
        laserDamageEffect.Stop();
        laserDamage = startlaserDamage;
        laserSlowingFactor = startlaserSlowingFactor;
    }

    public void SetBulletTransform(Transform[] firePointTransform)
    {
        this.firePointTransform = firePointTransform;
    }

    public void SetTargetComponent(Transform transform, UnitHealthSystem unitHealth, EnemyMovement enemyMovement)
    {
        currentTargetTransform = transform;
        targetUnitHealthSystem = unitHealth;
        targetEnemyMovement = enemyMovement;
    }

    public void Shoot()
    {
        DealLaserDamage();
        TurnOnLineRenderer();
        StartCoroutine(TurnOnLaserParticalSystem());
    }

    public void StopShoot()
    {
        lineRenderer.enabled = false;
        laserDamageLightEffect.enabled = false;
        laserDamageEffect.Stop();
    }

    private void DealLaserDamage()
    {
        targetEnemyMovement.ChangeSpeed(laserSlowingFactor);
        currentDeltaTimeLaserDamage = laserDamage * Time.deltaTime;
        targetUnitHealthSystem.DealDamage(currentDeltaTimeLaserDamage);
    }

    private void TurnOnLineRenderer()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePointTransform[0].position);
        lineRenderer.SetPosition(1, currentTargetTransform.position);
    }

    private IEnumerator TurnOnLaserParticalSystem()
    {
        if (!laserDamageLightEffect.enabled == true)
        {
            laserDamageLightEffect.enabled = true;
            laserDamageEffect.Play();
        }
        Vector3 damageEffectDirection = firePointTransform[0].position -
            laserDamageEffect.transform.position;
        Vector3 damageEffectDirectionOffset = damageEffectDirection.normalized * 1.1f;

        laserDamageEffect.transform.rotation = Quaternion.LookRotation(damageEffectDirection);
        laserDamageEffect.transform.position = currentTargetTransform.position +
            damageEffectDirectionOffset;
        yield return null;
    }

    public void UpgradeTurret()
    {
        laserDamage *= 1.1f;
        laserSlowingFactor *= 1.1f;
    }
}
