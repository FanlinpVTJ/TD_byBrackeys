using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField] private Vector3 turretOnNodeOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject buildEffectPrefab;
    [SerializeField] private TurretBlueprint[] UpgradePrefub;
    [SerializeField] private PlayerStats playerStats;

    private TurretBlueprint turretBlueprint;
    private TurretNodeBuilder turretNodeBuilder;
   
    private void Start()
    {
        turretNodeBuilder = GetComponent<TurretNodeBuilder>();
    }

    public void Upgrade(TurretBlueprint turretBlueprint)
    {
        this.turretBlueprint = turretBlueprint;
        UpgradeTurret();
    }

    public void UpgradeTurret()
    {
        if (turretBlueprint.TurretLevel >= turretBlueprint.MaxTurretLevel)
        {
            turretNodeBuilder.Turret.GetComponent<IDamageType>().UpgradeTurret();
            Debug.Log("Upgrade with stats");
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

        turretNodeBuilder.SetTurretBlueprint(turretBlueprint);
        turretNodeBuilder.TurretBuilding();

    }
}
