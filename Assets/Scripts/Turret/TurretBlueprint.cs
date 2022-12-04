using UnityEngine;
using System;

[Serializable]
public class TurretBlueprint : MonoBehaviour
{
    [SerializeField] private TurretBlueprint turretToUpgrade;
    [SerializeField] private int cost;
    [SerializeField] private int turretLevel;
    [SerializeField] private int upgradeCost;
    [SerializeField] private int maxThisTypeTurretLevel;
    [SerializeField] private int sellCostCoeff;

    public TurretBlueprint TurretToUpgrade { get { return turretToUpgrade; } }
    public int Cost { get { return cost; } }
    public int TurretLevel { get { return turretLevel; } }
    public int UpgradeCost { get { return upgradeCost; } }
    public int SellCost { get { return cost/ sellCostCoeff; } }
    public int MaxThisTypeTurretLevel { get { return maxThisTypeTurretLevel; } }
    public IDamageType IDamageType { get { return GetComponent<IDamageType>(); } }

}
