using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: окей, прикольно, что он выполняет свою функцию
// не окей, что он тоже меняет баланс в _wallet, это бы тоже делать по ивенту
// Понравилось, что сначала Select, потом BuildOn(node)
// название +, названия методов тоже +
public class BuildingManager : MonoBehaviour
{
    // TODO: а-я-яй
    // хотя-бы сделай, чтобы нельзя было извне инстанс поменять, чтоли, раз уж синглтон сделал
    // public static BuildingManager Instance { get; private set; }
    // private set значит, что только внутри класса можно это поменять
    //Singleton
    public static BuildingManager instance; // TODO: публичное PascalCase - Instance (с большой буквы)

    // TODO: сериализованные поля должны лежать выше всех остальных полей и свойств
    [SerializeField] private Vector3 _turretOnNodeOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private GameObject _buitldEffectPrefab;

    private TurretBlueprint _turretToBuild;
    public bool _canBuild { get { return _turretToBuild != null; } } // TODO: публичное PascalCase
    public bool _hasMoney { get { return PlayerStats._wallet >= _turretToBuild.cost; } } // TODO: публичное PascalCase
    
    // TODO: можно вот так было, через стрелочку, чтоб скобочек меньше писать
    // это когда у тебя есть только get
    // public bool _canBuild => _turretToBuild != null;
    // public bool _hasMoney => PlayerStats._wallet >= _turretToBuild.cost;

    private void Awake()
    {
        instance = this;
    }

    // TODO: параметры в camelCase turretToBuild
    public void SelectTurretToBuild(TurretBlueprint _turretToBuild)
    {
        this._turretToBuild = _turretToBuild;
    }
    public void BuildTurretOn(NodeSelectionBuild node)
    {
        if(PlayerStats._wallet < _turretToBuild.cost)
        {
            Debug.Log("no money no honey u tebya" + PlayerStats._wallet); // лол))
            return;
        }

        PlayerStats._wallet -= _turretToBuild.cost; 
        GameObject turret = Instantiate(_turretToBuild.prefab, node.transform.position + _turretOnNodeOffset, Quaternion.identity);
        // TODO: локальные переменные тоже camelCase buildEffect
        GameObject _buitldEffect = Instantiate(_buitldEffectPrefab, node.transform.position + _turretOnNodeOffset, Quaternion.identity);
        Destroy(_buitldEffect, 2f);
        node._turret = turret;
        Debug.Log("Turret build, money left:" + PlayerStats._wallet);
    }
}

