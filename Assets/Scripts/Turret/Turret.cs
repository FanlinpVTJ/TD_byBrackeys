using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform partToRotate;
    [SerializeField] private float _turretFireRange = 15f;
    [SerializeField] private float speedRotationOfTurret = 15f;
   
    private ITrigger trigger;
    private UnitHealthSystem targetUnitHealthSystem;
    private EnemyMovement targetEnemyMovement;
    private Transform targetTransform;
    private GameObject target;

    private void Start()
    {
        StartCoroutine(TargetingEnemy());
        trigger = GetComponent<ITrigger>();
    }

    private void Update()
    {
        if (target != null)
        {
            targetUnitHealthSystem = target.GetComponent<UnitHealthSystem>();
            targetEnemyMovement = target.GetComponent<EnemyMovement>();
            targetTransform = target.GetComponent<Transform>();
            StartCoroutine(PullTrigger());
            LookAtTarget();
            return;
        }
        else
        {
            ReleaseTrigger();
        }
    }

    private IEnumerator PullTrigger()
    {
        yield return null;
        if (targetTransform != null && targetUnitHealthSystem != null && targetEnemyMovement != null)
        {
            trigger.Shoot(targetTransform, targetUnitHealthSystem, targetEnemyMovement);
        }
    }

    private void ReleaseTrigger()
    {
        trigger.StopShoot();
    }

    private void LookAtTarget()
    {
        var targetRotation = Quaternion.LookRotation(target.transform.position - partToRotate.position);
        Quaternion yrotationTurret = new Quaternion(0, partToRotate.rotation.y, 0, partToRotate.rotation.w);
        Quaternion yTargetRotation = new Quaternion(0, targetRotation.y, 0, targetRotation.w);
        partToRotate.rotation = Quaternion.Slerp(yrotationTurret, yTargetRotation, speedRotationOfTurret * Time.deltaTime);
    }

    private IEnumerator TargetingEnemy()
    {
        while (true)
        {
            GameObject _nearestEnemy = null;
            var turretFireRange = _turretFireRange;
            var targets = EnemyContainer.ListOfEnemies;

            foreach (var enemy in targets)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < turretFireRange)
                {
                    turretFireRange = distanceToEnemy;
                    _nearestEnemy = enemy.gameObject;
                }
            }
            if (_nearestEnemy != null && turretFireRange <= _turretFireRange)
            {
                target = _nearestEnemy.gameObject;
            }
            else
            {
                target = null;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
