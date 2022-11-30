using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodePointer : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color hoverColorUpgrade;
    [SerializeField] private Color notEnoughtMoneyColor;

    private Color startColor;
    private Renderer nodeRenderer;
    private TurretBuildInput turretBuildInput;
    private void Start()
    {
        turretBuildInput = GetComponent<TurretBuildInput>();
        nodeRenderer = GetComponent<Renderer>();
        startColor = nodeRenderer.material.color;
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!BuildingManager.Instance.CanBuild)
        {
            return;
        }
        if (BuildingManager.Instance.HasMoney && turretBuildInput.Turret == null) 
        {
            nodeRenderer.material.color = hoverColor;
        }
        else if (turretBuildInput.Turret != null)
        {
            nodeRenderer.material.color = hoverColorUpgrade;
        }
        else
        {
            nodeRenderer.material.color = notEnoughtMoneyColor;
        }
    }
    private void OnMouseExit()
    {
        nodeRenderer.material.color = startColor;
    }
}
