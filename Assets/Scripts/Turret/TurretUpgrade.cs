using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField] private Vector3 turretOnNodeOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject buildEffectPrefab;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private TurretNodeBuilder turretNodeBuilder;
    
    private TurretBlueprint turretBlueprint;

    public void Upgrade(TurretBlueprint turretBlueprint)
    {
        this.turretBlueprint = turretBlueprint;
        UpgradeTurret();
    }

    public void UpgradeTurret()
    {
        if (turretBlueprint.TurretLevel >= turretBlueprint.MaxThisTypeTurretLevel)
        {
            turretBlueprint.IDamageType.UpgradeTurret();
        }
        else
        {
            UpgradeWithPrefub();
        }
    }

    private void UpgradeWithPrefub()
    {
        if (!BuildingManager.Instance.HasMoneyToUpgrade)
        {
            return;
        }
        Destroy(turretNodeBuilder.Turret);
        turretNodeBuilder.SetTurretBlueprint(turretBlueprint.TurretToUpgrade);
        Debug.Log(turretBlueprint.TurretToUpgrade);
        turretNodeBuilder.TurretBuilding();
    }
}
