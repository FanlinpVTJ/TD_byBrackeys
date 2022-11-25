using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretTrigger : MonoBehaviour, ITrigger
{
    [SerializeField] private Transform[] firePointTransform;
    
    private IDamageType damageType;

    private void Start()
    {
        damageType = GetComponent<IDamageType>();
    }
    public void Shoot(Transform transform, UnitHealthSystem unitHealth, EnemyMovement enemyMovement)
    {
        damageType.SetBulletTransform(firePointTransform);
        damageType.SetTargetComponent(transform, unitHealth, enemyMovement);
        damageType.Shoot();
    }
    public void StopShoot()
    {
        damageType.StopShoot();
    }
}
