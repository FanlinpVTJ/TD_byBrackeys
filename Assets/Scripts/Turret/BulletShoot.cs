using System.Collections;
using UnityEngine;

public class BulletShoot : MonoBehaviour, IDamageType
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float startfireRate;
    [SerializeField] private float startbulletDamage;

    private float fireCountdown;
    private Transform[] firePointTransform;
    private bool IsReload = false;
    private UnitHealthSystem targetUnitHealthSystem;
    private Transform currentTargetTransform;
    private float fireRate;
    private float bulletDamage;

    private void Start()
    {
        fireCountdown = startfireRate;
        fireRate = startfireRate;
        bulletDamage = startbulletDamage;
    }
    private void Update()
    {
        if (fireCountdown <= 0)
        {
            IsReload = true;
            fireCountdown = fireRate;
        }
        else
        {
            IsReload = false;
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
        if (IsReload)
        {
            foreach (var firePointTransform in firePointTransform)
            {
                GameObject bulletGameObject = Instantiate(this.bullet, firePointTransform);
                Bullet bullet = bulletGameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.ShotBullet(currentTargetTransform, targetUnitHealthSystem);
                    bullet.SetBulletDamage(bulletDamage);
                }
                yield return new WaitForSeconds(fireRate/ this.firePointTransform.Length);
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

    public void UpgradeTurret()
    {
        fireRate *= 0.9f;
        bulletDamage *= 1.1f;
    }
}

