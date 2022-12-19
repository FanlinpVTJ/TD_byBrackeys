using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TurretList turretList;
    [SerializeField] private ShopItem shopItemPrefab;
    [SerializeField] private Transform itemsParent;

    private List<ShopItem> items = new();

    private void Start()
    {
        foreach (var turret in turretList.AllTurrets)
        {
            CreateItem(turret);
        }
    }

    private void OnDestroy()
    {
        foreach(var item in items)
        {
            item.OnClicked -= HandleShopItemClicked;
        }
    }

    private ShopItem CreateItem(TurretConfig config)
    {
        var instance = Instantiate(shopItemPrefab, itemsParent);
        instance.Initialize(config);
        items.Add(instance);
        instance.OnClicked += HandleShopItemClicked;
        return instance;
    }

    private void HandleShopItemClicked(ShopItem item)
    {
        Debug.Log(item.Config.name);
    }

    public void SelectTurretLvl1()
    {
        //BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[0]);
    }

    public void SelectTurretLvl2()
    {
        //BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[1]);
    }

    public void SelectTurretLvl3()
    {
        //BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[2]);
    }

    public void SelectTurretMissleLauncher()
    {
        //BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[3]);
    }

    public void SelectLaserTurret()
    {
        //BuildingManager.Instance.SetTurretToBuild(_turretsBlueprints[4]);
    }
}
