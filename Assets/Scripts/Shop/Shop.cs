using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// TODO: почему Main?))
// не советовал бы делать из кнопки вызов метода в редакторе, но в целом пойдет)
public class Shop : MonoBehaviour
{
    [SerializeField] private TurretBlueprint[] _turretsBlueprints; 
    [SerializeField] private TextMeshProUGUI[] _turretsCost;

    private void Start()
    {
        for (int i = 0; i < _turretsCost.Length; i++)
        {
            _turretsCost[i].text = "$" + _turretsBlueprints[i].Cost.ToString();
        }
    }
    public void SelectTurretLvl1()
    {
        BuildingManager.Instance.SelectTurretToBuild(_turretsBlueprints[0]);
    }
    public void SelectTurretLvl2()
    {
        BuildingManager.Instance.SelectTurretToBuild(_turretsBlueprints[1]);
    }
    public void SelectTurretLvl3()
    {
        BuildingManager.Instance.SelectTurretToBuild(_turretsBlueprints[2]);
    }
    public void SelectTurretMissleLauncher()
    {
        BuildingManager.Instance.SelectTurretToBuild(_turretsBlueprints[3]);
    }
    public void SelectLaserTurret()
    {
        BuildingManager.Instance.SelectTurretToBuild(_turretsBlueprints[4]);
    }
}
