using System;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public event Action<int> OnTurretBuilding;

    [SerializeField] private Vector3 turretOnNodeOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject buildEffectPrefab;
    [SerializeField] private NodeUI nodeUI;
    private PlayerStats playerStats;

    public static BuildingManager Instance { get; private set; }
    public bool CanBuild => turretToBuild != null;
    public bool HasMoney => playerStats.PlayerMoney >= turretToBuild.Cost;

    private TurretBlueprint turretToBuild;
    private TurretBuildInput selectedNode;

    private void Awake()
    {
        Instance = this;
        playerStats = GetComponent<PlayerStats>();
    }
    
    public void SelectTurretToBuild(TurretBlueprint turretToBuild)
    {
        this.turretToBuild = turretToBuild;
        DeselectNode();
    }

    public void SelectTurretToUpgradeOrSell(TurretBuildInput selectedNode)
    {
        if (this.selectedNode == selectedNode)
        {
            DeselectNode();
            return;
        }
        this.selectedNode = selectedNode;
        turretToBuild = null;
        nodeUI.SetTarget(this.selectedNode);
    }

    private void DeselectNode()
    {
        nodeUI.Hide();
        selectedNode = null;
    }
    public void BuildTurretOn(TurretBuildInput node)
    {
        if(!HasMoney)
        {
            return;
        }

        GameObject turret = Instantiate(turretToBuild.Prefab, node.transform.position + turretOnNodeOffset, 
            Quaternion.identity);
        GameObject _buitldEffect = Instantiate(buildEffectPrefab, node.transform.position + turretOnNodeOffset, 
            Quaternion.identity);
        Destroy(_buitldEffect, 2f);
        node.Turret = turret;
        node.TurretCost = turretToBuild.Cost;
        OnTurretBuilding.Invoke(-turretToBuild.Cost);
    }
}

