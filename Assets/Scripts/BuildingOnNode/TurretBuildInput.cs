using UnityEngine;
using UnityEngine.EventSystems;

public class TurretBuildInput : MonoBehaviour
{
    public GameObject Turret { get; set; }
    public int TurretCost { get; set; }
    
   
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Turret != null)
        {
            BuildingManager.Instance.SelectTurretToUpgradeOrSell(this);
            return;
        }
        if (!BuildingManager.Instance.CanBuild)
            return;
        BuildingManager.Instance.BuildTurretOn(this);
    }
 }