using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    //Singleton
    public static BuildingManager instance;

    [SerializeField] private Vector3 _turretOnNodeOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject _buitldEffectPrefab;

    private TurretBlueprint _turretToBuild;
    public bool _canBuild { get { return _turretToBuild != null; } }
    public bool _hasMoney { get { return PlayerStats._wallet >= _turretToBuild.cost; } }

    private void Awake()
    {
        instance = this;
    }

    public void SelectTurretToBuild(TurretBlueprint _turretToBuild)
    {
        this._turretToBuild = _turretToBuild;
    }
    public void BuildTurretOn(NodeSelectionBuild node)
    {
        if(PlayerStats._wallet < _turretToBuild.cost)
        {
            Debug.Log("no money no honey u tebya" + PlayerStats._wallet);
            return;
        }

        PlayerStats._wallet -= _turretToBuild.cost; 
        GameObject turret = Instantiate(_turretToBuild.prefab, node.transform.position + _turretOnNodeOffset, Quaternion.identity);
        GameObject _buitldEffect = Instantiate(_buitldEffectPrefab, node.transform.position + _turretOnNodeOffset, Quaternion.identity);
        Destroy(_buitldEffect, 2f);
        node._turret = turret;
        Debug.Log("Turret build, money left:" + PlayerStats._wallet);
    }
}

