using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeUITexts : MonoBehaviour
{
    [SerializeField] private NodeUI nodeUI;
    [SerializeField] private TextMeshProUGUI UpgradeCostText;
    [SerializeField] private TextMeshProUGUI SellCostText;

    public void SetUpgradeAndSellCosts(TurretNodeBuilder turretNodeBuilder)
    {
        UpgradeCostText.text = "$"+ turretNodeBuilder.TurretUpgradeCost.ToString();
        SellCostText.text = "$"+turretNodeBuilder.TurretSellCost.ToString();
    }
    
        



}
