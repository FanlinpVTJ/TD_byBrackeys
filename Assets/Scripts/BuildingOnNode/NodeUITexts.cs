using TMPro;
using UnityEngine;

public class NodeUITexts : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UpgradeCostText;
    [SerializeField] private TextMeshProUGUI SellCostText;

    public void SetUpgradeAndSellCosts(TurretNodeBuilder turretNodeBuilder)
    {
        UpgradeCostText.text = "$" + turretNodeBuilder.TurretUpgradeCost.ToString();
        SellCostText.text = "$" + turretNodeBuilder.TurretSellCost.ToString();
    }
}
