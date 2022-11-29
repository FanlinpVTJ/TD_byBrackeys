using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthSystem : MonoBehaviour
{   
    public event Action<UnitHealthSystem> OnDeath;
    public event Action<int> OnDeathChangeMoney;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private int costForDestroy = 10;
    [SerializeField] private Image hpBar;

    private float currentHealth;
    private bool isOnDeathChangeMoneyAwake = true;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void DealDamage(float bulletDamage)
    {
        var damagedHealth = currentHealth - bulletDamage;
        if (damagedHealth <= 0)
        {
            StartCoroutine(UnitDeath());
            return;
        }
        currentHealth = Mathf.Max(0, damagedHealth);
        hpBar.fillAmount = currentHealth / maxHealth;
    }

    private IEnumerator UnitDeath()
    {
        yield return null;
        if (isOnDeathChangeMoneyAwake)
        {
            OnDeathChangeMoney?.Invoke(costForDestroy);
            isOnDeathChangeMoneyAwake = false;
        }
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        OnDeath?.Invoke(this);
    }
}
