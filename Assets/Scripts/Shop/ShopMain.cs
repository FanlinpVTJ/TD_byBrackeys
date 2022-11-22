using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TODO: почему Main?))
// не советовал бы делать из кнопки вызов метода в редакторе, но в целом пойдет)
public class ShopMain : MonoBehaviour
{
    [SerializeField] private TurretBlueprint[] _turretsBlueprints; 
    [SerializeField] private TextMeshProUGUI[] _turretsCost;

    // красава private не забыл))
    private void Start()
    {
        for (int i = 0; i < _turretsCost.Length; i++)
        {
            _turretsCost[i].text = "$" + _turretsBlueprints[i].cost.ToString();
        }
    }
    
    // TODO: _ в названиях методов - это уже питон какой-то, не надо так))
    // TODO: LVL можно было написать Lvl, чем капс лучше?
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
    
    // TODO: что за 2t?
    public void SelectMissle_Launcher_lvl_2t() // оу бой, прям реально питон почти. по-питонски было бы select_missile_launcher_lvl_2t
    {
        BuildingManager.instance.SelectTurretToBuild(_turretsBlueprints[3]);
    }
    public void SelectLaserTurret()
    {
        BuildingManager.instance.SelectTurretToBuild(_turretsBlueprints[4]);
    }
}
