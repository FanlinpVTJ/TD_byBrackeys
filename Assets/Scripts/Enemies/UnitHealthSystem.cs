using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO: почему именно EnemyHealth? компонент не подходит для чего-то другого, кроме Enemy?
// Вижу, что эффектик инстансится, но это можно было бы вынести куда-то еще
public class UnitHealthSystem : MonoBehaviour
{
    public event Action<int> OnDeath;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private int costForDestroy = 10;
    [SerializeField] private Image hpBar;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // TODO: параметры методов пишутся обычным camelCase, без _, float bulletDamage 
    public void DealDamage(float bulletDamage)
    {
        // TODO: вообще стоит делать ограничения на таких параметрах, чтобы они не уходили в минус
        // сделать это просто, можно без всяких if
        // _currentEnemyHealth = Mathf.Max(0, _currentEnemyHealth - _bulletDamage);
        // это будет работать так: максимальное число между 0 и результатом вычислений, т.е. не даст выйти за 0 в минус 
        var damagedHealth = currentHealth - bulletDamage;
        if (damagedHealth <= 0)
        {
            StartCoroutine(UnitDeath());
        }
        currentHealth = Mathf.Max(0, damagedHealth);
        hpBar.fillAmount = currentHealth / maxHealth;

        // TODO: естесстенно тут может не сработать, если сначала сдеать Max(0, health)
        // поэтому надо сначала записать новый хп в переменную, сделать эту проверку, а потом уже записать 
        // _currentEnemyHealth = Max(0, tempHealth)
    }

    private IEnumerator UnitDeath()
    {
        OnDeath?.Invoke(costForDestroy);
        yield return null;
        Destroy(gameObject);
    }
}
