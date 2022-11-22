using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Vector3 _turretOnNodeOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject _buitldEffectPrefab;
    [SerializeField] private NodeUI nodeUI;
    //Singleton
    public static BuildingManager instance { get; private set; }

    private TurretBlueprint turretToBuild;
    private NodeSelectionBuild selectedNode;

    public bool _canBuild { get { return turretToBuild != null; } }
    public bool _hasMoney { get { return PlayerStats.wallet >= turretToBuild.cost; } }

    private void Awake()
    {
        instance = this;
    }

    public void SelectTurretToBuild(TurretBlueprint turretToBuild)
    {
        this.turretToBuild = turretToBuild;
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectNode(NodeSelectionBuild node)
    {
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }

    public void BuildTurretOn(NodeSelectionBuild node)
    {
        if(PlayerStats.wallet < turretToBuild.cost)
        {
            Debug.Log("no money no honey u tebya" + PlayerStats.wallet);
            return;
        }

        PlayerStats.wallet -= turretToBuild.cost; 
        GameObject turret = Instantiate(turretToBuild.prefab, node.transform.position + _turretOnNodeOffset, Quaternion.identity);
        GameObject _buitldEffect = Instantiate(_buitldEffectPrefab, node.transform.position + _turretOnNodeOffset, Quaternion.identity);
        Destroy(_buitldEffect, 2f);
        node.turret = turret;
        Debug.Log("Turret build, money left:" + PlayerStats.wallet);
    }
}

