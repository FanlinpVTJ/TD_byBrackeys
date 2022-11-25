using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: окей, прикольно, что он выполняет свою функцию
// не окей, что он тоже меняет баланс в _wallet, это бы тоже делать по ивенту
// Понравилось, что сначала Select, потом BuildOn(node)
// название +, названия методов тоже +
public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Vector3 turretOnNodeOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject buildEffectPrefab;
    
    public static BuildingManager Instance { get; private set; }// TODO: публичное PascalCase - Instance (с большой буквы)
    public bool CanBuild => turretToBuild != null; // TODO: публичное PascalCase
    public bool HasMoney => PlayerStats.PlayerMoney >= turretToBuild.Cost; // TODO: публичное PascalCase

    private TurretBlueprint turretToBuild;

    private void Awake()
    {
        Instance = this;
    }
    
    public void SelectTurretToBuild(TurretBlueprint turretToBuild)
    {
        this.turretToBuild = turretToBuild;
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
    }
}

