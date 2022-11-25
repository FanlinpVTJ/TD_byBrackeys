using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform partToRotate;
    [SerializeField] private string _tagToFind = "Enemy";
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

    // TODO: а вот это как раз можно было в апдейте сделать)
    // оно непрерывное, у этого нет окончания
    // хотя судя по WaitForSeconds(0.2f), я так понимаю, тут такая задумка, что не каждый кадр проверки?
    // или это чтоб не лагало от поиска по тегу?))

    //Да, шоб не лагало) Эвенты будут следующей иттерацией
    private IEnumerator TargetingEnemy()
    {
        while (true)
        {
            GameObject _nearestEnemy = null;
            var shortestDistance = _turretFireRange;
            
            // TODO: оу май, поиск по тэгу))
            //Да, нужно строитть систему эвентов для регуляции спавна и смерти, но я пока не владею этим
            GameObject[] targets = GameObject.FindGameObjectsWithTag(_tagToFind);
            foreach (var enemy in targets)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    _nearestEnemy = enemy;
                }
            }
            if (_nearestEnemy != null && shortestDistance <= _turretFireRange)
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _turretFireRange);
    }
}
