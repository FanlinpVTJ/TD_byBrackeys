using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class TurretNodeBuilder : MonoBehaviour
{
    [SerializeField] private Vector3 turretOnNodeOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject buildEffectPrefab;
    [SerializeField] private GameObject sellEffectPrefab;

    public GameObject Turret { get; set; }
    public int TurretCost { get; private set; }
    public int TurretUpgradeCost { get; private set; }
    public int TurretSellCost { get; private set; }


    private TurretBlueprint turretBlueprint;
    private TurretUpgrade turretUpgrade;

    private void Start()
    {
        turretUpgrade = GetComponent<TurretUpgrade>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Turret != null)
        {
            BuildingManager.Instance.SelectTurretToUpgradeOrSell(this);
            return;
        }
        if (!BuildingManager.Instance.CanBuild)
            return;
        BuildingManager.Instance.BuildTurretOn(this);
        TurretBuilding();
    }

    public void SetTurretBlueprint(TurretBlueprint turretBlueprint)
    {
        this.turretBlueprint = turretBlueprint;
    }

    public void TurretBuilding()
    {
        GameObject turret = Instantiate(turretBlueprint.gameObject, transform.position + turretOnNodeOffset,
            Quaternion.identity);
        GameObject _buitldEffect = Instantiate(buildEffectPrefab, transform.position + turretOnNodeOffset,
            Quaternion.identity);
        Destroy(_buitldEffect, 2f);
        Turret = turret;
        TurretCost = turretBlueprint.Cost;
        TurretSellCost = turretBlueprint.SellCost;
        TurretUpgradeCost = turretBlueprint.UpgradeCost;
    }

    public void UpgradeTurret()
    {
        turretUpgrade.Upgrade(turretBlueprint);
    }

    public void SellTurret()
    {
        Destroy(Turret);
        GameObject _buitldEffect = Instantiate(sellEffectPrefab, transform.position + turretOnNodeOffset,
            Quaternion.identity);
        Destroy(_buitldEffect, 2f);
        Turret = null;
        TurretCost = 0;
        TurretSellCost = 0;
        TurretUpgradeCost = 0;
    }
 }