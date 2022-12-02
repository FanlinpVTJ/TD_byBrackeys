using System;
using UnityEngine;

//�������� ������ ������,
//�������� � �����,
//�������� ����� ���������,
//�������� �� ����������� NodeUI
//����� ���� �� turret build input
//��� � ���� �����, �� ���� ��� ����� ������������ ��� ���, ���� ����� ��������� �����,
//���� �� ������������ ��� ���. � ����� ���� ��������, ��� ������ �������������� � ���� �� ���������� �� � ��������� ��)
public class BuildingManager : MonoBehaviour
{
    public event Action<int> OnTurretBuilding;
   
    [SerializeField] private NodeUI nodeUI;
    private PlayerStats playerStats;

    public static BuildingManager Instance { get; private set; }
    public bool CanBuild => turretBlueprint != null;
    public bool HasMoney => playerStats.PlayerMoney >= turretBlueprint.Cost;
    public bool HasMoneyToUpgrade => playerStats.PlayerMoney >= turretUpgradeCost;

    private TurretBlueprint turretBlueprint;
    private TurretNodeBuilder turretNodeBuilder;
    private int turretUpgradeCost;

    private void Awake()
    {
        Instance = this;
        playerStats = GetComponent<PlayerStats>();
    }

    public void SetTurretUpgradeCost(int turretUpgradeCost)
    {
        this.turretUpgradeCost = turretUpgradeCost; 
    }
    
    public void SetTurretToBuild(TurretBlueprint turretBlueprint)
    {
        this.turretBlueprint = turretBlueprint;
    }

    public void SelectTurretToUpgradeOrSell(TurretNodeBuilder turretNodeBuilder)
    {
        if (this.turretNodeBuilder == turretNodeBuilder)
        {
            DeselectNode();
            return;
        }
        this.turretNodeBuilder = turretNodeBuilder;
        turretBlueprint = null;
        nodeUI.SetTarget(this.turretNodeBuilder);
    }

    private void DeselectNode()
    {
        nodeUI.HideNodeUI();
        turretNodeBuilder = null;
    }

    public void BuildTurretOn(TurretNodeBuilder turretBuildInput)
    {
        if(!HasMoney)
        {
            return;
        }
        turretBuildInput.SetTurretBlueprint(turretBlueprint);
        OnTurretBuilding.Invoke(-turretBlueprint.Cost);
    }
}

