using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NodePainter : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color hoverColorUpgrade;
    [SerializeField] private Color notEnoughtMoneyColor;

    private Color startColor;
    private Renderer nodeRenderer;
    private TurretNodeBuilder turretBuildInput;
    private bool IsMouseOnNode = false;

    private void Start()
    {
        turretBuildInput = GetComponent<TurretNodeBuilder>();
        nodeRenderer = GetComponent<Renderer>();
        startColor = nodeRenderer.material.color;
    }

    private void Update()
    {
        if (IsMouseOnNode)
        {
            UpdateColor();
        }
    }

    private void OnMouseEnter()
    {
        IsMouseOnNode=true;
    }

    private void OnMouseExit()
    {
        nodeRenderer.material.color = startColor;
        IsMouseOnNode = false;
    }

    public void UpdateColor()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!BuildingManager.Instance.CanBuild)
        {
            return;
        }
        if (BuildingManager.Instance.HasMoney && !turretBuildInput.Turret)
        {
            nodeRenderer.material.color = hoverColor;
        }
        else if (turretBuildInput.Turret)
        {
            nodeRenderer.material.color = hoverColorUpgrade;
        }
        else
        {
            nodeRenderer.material.color = notEnoughtMoneyColor;
        }
    }
}
