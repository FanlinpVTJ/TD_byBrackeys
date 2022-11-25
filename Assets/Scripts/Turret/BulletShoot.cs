using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour, IDamageType
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate = 0.5f;

    private float fireCountdown;
    private Transform[] firePointTransform;
    private bool canShoot = false;
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
            canShoot = true;
            fireCountdown = fireRate;
        }
        else
        {
            canShoot = false;
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
        if (canShoot)
        {
            foreach (var firePointTransform in firePointTransform)
            {
                GameObject bulletGameObject = Instantiate(this.bullet, firePointTransform);
                Bullet bullet = bulletGameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.ShotBullet(currentTargetTransform);
                    bullet.SetUnitHealthSystem(targetUnitHealthSystem);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return null;
    }

    public void StopShoot()
    {
        StopAllCoroutines();
    }
}

