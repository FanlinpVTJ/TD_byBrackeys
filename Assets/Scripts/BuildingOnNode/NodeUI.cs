using System;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public event Action<int> OnSellTurret;
    public event Action<int> OnUpgradeTurret;
    [SerializeField] private int sellCoefficient = 2;

    private TurretNodeBuilder turretNodeBuilder;

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    public void SetTarget(TurretNodeBuilder turretNodeBuilder)
    {
        gameObject.SetActive(true);
        this.turretNodeBuilder = turretNodeBuilder;
        transform.position = this.turretNodeBuilder.transform.position;
    }

    public void HideNodeUI()
    {
        gameObject.SetActive(false);
    }

    public void SellTurret()
    {
        Debug.Log("Sell from NodeUI");
        var sellCost = turretNodeBuilder.TurretCost / sellCoefficient;
        OnSellTurret.Invoke(sellCost);
        Destroy(turretNodeBuilder.Turret);
        turretNodeBuilder =null;
        HideNodeUI();
    }

    public void UpgradeTurret()
    {
        Debug.Log("Update from NodeUI");
        var UpgradelCost = turretNodeBuilder.TurretUpgradeCost;
        BuildingManager.Instance.SetTurretUpgradeCost(UpgradelCost);
        if (!BuildingManager.Instance.HasMoneyToUpgrade)
        {
            return;
        }
        OnUpgradeTurret.Invoke(-UpgradelCost);
        turretNodeBuilder.UpgradeTurret();
    }
}
