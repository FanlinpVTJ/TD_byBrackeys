using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour, IDamageType
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate = 0.5f;

    private float fireCountdown;
    private Transform[] firePointTransform;
    private bool isReload = false;
    private UnitHealthSystem targetUnitHealthSystem;
    private EnemyMovement targetEnemyMovement;
    private Transform currentTargetTransform;

    private void Start()
    {
        fireCountdown = fireRate;
    }
    private void Update()
    {
        if (fireCountdown <= 0)
        {
            isReload = true;
            fireCountdown = fireRate;
        }
        else
        {
            isReload = false;
        }
        fireCountdown -= Time.deltaTime;
    }
    public void SetBulletTransform(Transform[] firePointTransform)
    {
        this.firePointTransform = firePointTransform;
    }
    public void SetTargetComponent(Transform currentTargetTransform, UnitHealthSystem targetUnitHealthSystem, EnemyMovement enemyMovement)
    {
        this.currentTargetTransform = currentTargetTransform;
        this.targetUnitHealthSystem = targetUnitHealthSystem;
    }
    public void Shoot()
    {
        StartCoroutine(ShootQueue());
    }
    private IEnumerator ShootQueue()
    {
        if (isReload)
        {
            foreach (var firePointTransform in firePointTransform)
            {
                GameObject bulletGameObject = Instantiate(this.bullet, firePointTransform);
                Bullet bullet = bulletGameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.ShotBullet(currentTargetTransform, targetUnitHealthSystem);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return null;
    }

    public void StopShoot()
    {
        StopAllCoroutines();
        if (gameObject.GetComponentInChildren<Bullet>() != null)
            Destroy(GetComponentInChildren<Bullet>().gameObject);
    }
}

