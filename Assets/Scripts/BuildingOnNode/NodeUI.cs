using System;
using UnityEngine;

//��� ������� ������� ��� ��, �� ��� ����, ������ ��� ����� ������ ���:
//��������� ��������� ��������� ����, �������� ������ �� �������/�������
//����� ��� ������ � ��� ������ � ��� �������������� ������� ��� �������
//�� ��� ��������, � � ��������))
public class NodeUI : MonoBehaviour
{
    public event Action<int> OnSellTurret;
    public event Action<int> OnUpgradeTurret;
    [SerializeField] private NodeUITexts nodeUITexts;

    private TurretNodeBuilder turretNodeBuilder;

    public void SetTarget(TurretNodeBuilder turretNodeBuilder)
    {
        gameObject.SetActive(true);
        this.turretNodeBuilder = turretNodeBuilder;
        transform.position = this.turretNodeBuilder.transform.position;
        nodeUITexts.SetUpgradeAndSellCosts(turretNodeBuilder);
    }

    public void HideNodeUI()
    {
        gameObject.SetActive(false);
    }

    public void SellTurret()
    {
        OnSellTurret.Invoke(turretNodeBuilder.TurretSellCost);
        turretNodeBuilder.SellTurret();
        turretNodeBuilder =null;
        HideNodeUI();
    }

    public void UpgradeTurret()
    {
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
