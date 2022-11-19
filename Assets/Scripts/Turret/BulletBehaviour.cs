using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _impactEffect;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _explosionRadius = 0f;
    [SerializeField] private int _bulletDamage;

    private Transform _currentTargetTransform;

    private void Start()
    {
        gameObject.transform.parent = null;
    }

    private void Update()
    {
        if (_currentTargetTransform != null)
        {
            Vector3 direction = _currentTargetTransform.transform.position - transform.position;
            float distanceThisFame = _bulletSpeed * Time.deltaTime;
            if (direction.magnitude <= distanceThisFame)
            {
                HitTarget();
            }
            transform.Translate(direction.normalized * distanceThisFame, Space.World);
            transform.LookAt(_currentTargetTransform);
        }
        else Destroy(gameObject);
    }
    public void ShotBullet(Transform _currentTargetTransform)
    {
        this._currentTargetTransform = _currentTargetTransform;
    }
    private void HitTarget()
    {
        GameObject _particalEffect = Instantiate(_impactEffect, transform.position, transform.rotation);
        Destroy(_particalEffect, 2f);
        Destroy(gameObject);
       
        if (_explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            if (_currentTargetTransform.TryGetComponent(out EnemyHealth _enemyHealth))
            {
                Damage(_enemyHealth);
            }
        }
    }
    
    private void Explode()
    {
        Collider[] colliderCountaner = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (var enemy in colliderCountaner)
        {
            if (enemy.TryGetComponent(out EnemyHealth _enemyHealth))
            {
                Damage(_enemyHealth);
            }
        }
    }

    private void Damage(EnemyHealth enemy)
    {
        enemy.DealDamage(_bulletDamage);
    }
}
