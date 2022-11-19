using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopMain : MonoBehaviour
{
    [SerializeField] private TurretBlueprint[] _turretsBlueprints; 
    [SerializeField] private TextMeshProUGUI[] _turretsCost;

    private void Start()
    {
        for (int i = 0; i < _turretsCost.Length; i++)
        {
            _turretsCost[i].text = "$" + _turretsBlueprints[i].cost.ToString();
        }
    }
    public void SelectLVL1_Turret()
    {
        BuildingManager.instance.SelectTurretToBuild(_turretsBlueprints[0]);
    }
    public void SelectLVL2_Turret()
    {
        BuildingManager.instance.SelectTurretToBuild(_turretsBlueprints[1]);
    }
    public void SelectLVL3_Turret()
    {
        BuildingManager.instance.SelectTurretToBuild(_turretsBlueprints[2]);
    }
    public void SelectMissle_Launcher_lvl_2t()
    {
        BuildingManager.instance.SelectTurretToBuild(_turretsBlueprints[3]);
    }
    public void SelectLaserTurret()
    {
        BuildingManager.instance.SelectTurretToBuild(_turretsBlueprints[4]);
    }
}
