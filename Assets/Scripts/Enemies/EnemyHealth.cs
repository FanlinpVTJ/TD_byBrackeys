using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: почему именно EnemyHealth? компонент не подходит для чего-то другого, кроме Enemy?
// Вижу, что эффектик инстансится, но это можно было бы вынести куда-то еще
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
    
    // TODO: параметры методов пишутся обычным camelCase, без _, float bulletDamage 
    public void DealDamage(float _bulletDamage)
    {
        // TODO: вообще стоит делать ограничения на таких параметрах, чтобы они не уходили в минус
        // сделать это просто, можно без всяких if
        // _currentEnemyHealth = Mathf.Max(0, _currentEnemyHealth - _bulletDamage);
        // это будет работать так: максимальное число между 0 и результатом вычислений, т.е. не даст выйти за 0 в минус 
        _currentEnemyHealth -= _bulletDamage;
        _hpBar.fillAmount = _currentEnemyHealth / _maxEnemyHealth;
        
        // TODO: естесстенно тут может не сработать, если сначала сдеать Max(0, health)
        // поэтому надо сначала записать новый хп в переменную, сделать эту проверку, а потом уже записать 
        // _currentEnemyHealth = Max(0, tempHealth)
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
