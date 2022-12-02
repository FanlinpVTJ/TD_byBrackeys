using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField] private Vector3 turretOnNodeOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject buildEffectPrefab;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private TurretNodeBuilder turretNodeBuilder;
    
    private TurretBlueprint turretBlueprint;
    private TurretBlueprint UpgradePrefub;
    

    public void Upgrade(TurretBlueprint turretBlueprint)
    {
        this.turretBlueprint = turretBlueprint;
        UpgradeTurret();
    }

    public void UpgradeTurret()
    {
        if (turretBlueprint.TurretLevel >= turretBlueprint.MaxThisTypeTurretLevel)
        {
            Debug.Log(turretBlueprint.IDamageType);
            turretBlueprint.IDamageType.UpgradeTurret();
        }
        else
        {
            Debug.Log("Upgrade with PREFAB");
            UpgradeWithPrefub(turretBlueprint.TurretLevel);
        }
    }

    private void UpgradeWithPrefub(int turretLevel)
    {
        if (!BuildingManager.Instance.HasMoneyToUpgrade)
        {
            return;
        }
        Destroy(turretNodeBuilder.Turret);
        turretNodeBuilder.SetTurretBlueprint(turretBlueprint.TurretToUpgrade);
        turretNodeBuilder.TurretBuilding();

    }
}
