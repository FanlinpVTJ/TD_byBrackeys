using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// тут как я понимаю две задачи, хоть и связанные, но все же разные: одна красит ноду, вторая строит турель
public class TurretBuildInput : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color notEnoughtMoneyColor;

    public GameObject Turret { get; set; } // optional - пометка, тк паблик высвечивается в инспекторе
    private Color startColor;
    private Renderer nodeRenderer;

    // TODO: private забыл
    private void Start()
    {
        nodeRenderer = GetComponent<Renderer>();
        startColor = nodeRenderer.material.color;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!BuildingManager.Instance.CanBuild)
            return;
        if (BuildingManager.Instance.HasMoney)
        {
            nodeRenderer.material.color = hoverColor;
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
    private void OnMouseDown()
    {
        if (!BuildingManager.Instance.CanBuild)
            return;
        if (Turret != null)
        {
            Destroy(Turret.gameObject);
            return;
        }
        BuildingManager.Instance.BuildTurretOn(this);
    }
 }