using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [SerializeField] private float explosionRadius = 5f;

    private void Start()
    {
        gameObject.transform.parent = null;
    }
    public override void Damage(UnitHealthSystem enemy)
    {
        Collider[] colliderCountaner = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var enemies in colliderCountaner)
        {
            if (enemies.TryGetComponent(out UnitHealthSystem enemiesHealth))
            {
                enemiesHealth.DealDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
    }
}
