using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretProject/UpgradeConfig")]
public class UpgadeConfig : ScriptableObject
{
    [SerializeField] private TurretConfig[] turretConfigs;
    public TurretConfig[] TurretConfigs => turretConfigs;
    public int MaxTurretLevel => turretConfigs.Length - 1;

    public int GetTurretLevel(TurretConfig turretConfig)
    {
        return Array.IndexOf(turretConfigs, turretConfig);
    }

    public float GetUpgradeCost(TurretConfig turretConfig)
    {
        return GetNextLevelConfig(turretConfig).Cost - turretConfig.Cost;
    }

    public TurretConfig GetNextLevelConfig(TurretConfig sourceConfig)
    {
        var level = GetTurretLevel(sourceConfig);
        var nextLevel = level + 1;
        return turretConfigs[nextLevel];
    }

    public bool CanUpgradeTurret(TurretConfig turretConfig)
    {
        var turretLevel = GetTurretLevel(turretConfig);
        return turretLevel < MaxTurretLevel;
    }
}
