using UnityEngine;
using System;

[Serializable]
public class TurretBlueprint
{
    public GameObject Prefab;
    public int Cost;
    public int TurretLevel;
    public int UpgradeCost;
    public int MaxTurretLevel;
    public IDamageType damageType;

}
