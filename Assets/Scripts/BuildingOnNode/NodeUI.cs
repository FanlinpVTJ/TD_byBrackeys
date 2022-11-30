using System;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public event Action<int> OnSellTurret;
    [SerializeField] private int sellCoefficient = 2;

    private TurretBuildInput selectedNode;

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    public void SetTarget(TurretBuildInput _node)
    {
        gameObject.SetActive(true);
        selectedNode = _node;
        transform.position = selectedNode.transform.position;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SellTurret()
    {
        var sellCost = selectedNode.TurretCost / sellCoefficient;
        OnSellTurret.Invoke(sellCost);
        Destroy(selectedNode.Turret.gameObject);
        selectedNode=null;
    }
}
