using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

// TODO: почему Behaviour? просто Bullet
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

    // TODO: шо мешало сделать корутину полета из точки A в точку B и после этого наносить дамаг?
    // опять же избавляешься от непрерывности, меньше разбираться что после чего будет вызываться
    // в корутине будет всё наглядно и линейно
    // цель -> долетел -> задамажил -> уничтожился
    // заодно избавишься от сравнений magnitude < distanceThisFrame,
    // потому что у тебя будет конец полета, когда цикл кончится
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
    
    // TODO: параметры без _ - currentTargetTransform
    public void ShotBullet(Transform _currentTargetTransform)
    {
        this._currentTargetTransform = _currentTargetTransform;
    }
    private void HitTarget()
    {
        GameObject _particalEffect = Instantiate(_impactEffect, transform.position, transform.rotation);
        Destroy(_particalEffect, 2f);
        Destroy(gameObject);
       
        // TODO: можно было бы сделать просто унаследованный класс ExplodableBullet и там взрываться без ифов
        // заодно радиус отсюда убрать
        if (_explosionRadius > 0)
        {
            Explode();
        }
        else
        {
            // TODO: локальные переменные без _ - enemyHealth
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
