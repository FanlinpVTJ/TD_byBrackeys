using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    void Shoot(Transform transform, UnitHealthSystem unitHealth, EnemyMovement enemyMovement);
    void StopShoot();
}
