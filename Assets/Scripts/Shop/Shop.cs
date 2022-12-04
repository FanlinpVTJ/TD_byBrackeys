using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TurretBlueprint[] _turretsBlueprints; 
    [SerializeField] private TextMeshProUGUI[] _turretsCost;

    private void Start()
    {
        for (int i = 0; i < _turretsCost.Length; i++)
        {
            Debug.Log(_turretsBlueprints[i].Cost.ToString());
            _turretsCost[i].text = "$" + _turretsBlueprints[i].Cost.ToString();
        }
    }

    public void SelectTurretLvl1()
    {
        BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[0]);
    }

    public void SelectTurretLvl2()
    {
        BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[1]);
    }

    public void SelectTurretLvl3()
    {
        BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[2]);
    }

    public void SelectTurretMissleLauncher()
    {
        BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[3]);
    }

    public void SelectLaserTurret()
    {
        BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[4]);
    }
}
