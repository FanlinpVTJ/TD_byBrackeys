using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageType
{
    void SetBulletTransform(Transform[] firePointTransform);
    void SetTargetComponent(Transform transform, UnitHealthSystem unitHealth, EnemyMovement enemyMovement);
    void Shoot();
    void StopShoot();
}
