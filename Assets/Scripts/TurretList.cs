using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "TurretProject/TurretList")]

public class TurretList : ScriptableObject
{
    [SerializeField] private UpgadeConfig[] upgradeConfigs;

    public UpgadeConfig[] UpgradeConfig => upgradeConfigs;

    public IEnumerable<TurretConfig> AllTurrets => upgradeConfigs.SelectMany(x => x.TurretConfigs);
}
