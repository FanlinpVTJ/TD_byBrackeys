using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _maxEnemyHealth = 100;
    [SerializeField] private int _costForEnemyDestroy = 10;
    [SerializeField] private Image _hpBar;
    [SerializeField] private GameObject _enemyDeathEffectPrefab;

    private float _currentEnemyHealth;

    private void Start()
    {
        _currentEnemyHealth = _maxEnemyHealth;
    }

    public void DealDamage(float _bulletDamage)
    {
        _currentEnemyHealth -= _bulletDamage;
        _hpBar.fillAmount = _currentEnemyHealth / _maxEnemyHealth;
        if (_currentEnemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        GameObject _enemyDeathEffect = Instantiate(_enemyDeathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(_enemyDeathEffect, 1f);
        PlayerStats.wallet += _costForEnemyDestroy;
        Destroy(gameObject);
    }
}
