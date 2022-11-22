using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeSelectionBuild : MonoBehaviour
{
    [SerializeField] private Color _hoverColor;
    [SerializeField] private Color _notEnoughtMoneyColor;

    [Header("Optional")]
    public GameObject turret;
    private Color _startColor;
    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _startColor = _renderer.material.color;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!BuildingManager.instance._canBuild)
            return;
        if (BuildingManager.instance._hasMoney)
        {
            _renderer.material.color = _hoverColor;
        }
        else
        {
            _renderer.material.color = _notEnoughtMoneyColor;
        }
    }
        
    private void OnMouseExit()
    {
        _renderer.material.color = _startColor;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (turret != null)
        {
            BuildingManager.instance.SelectNode(this);
            return;
        }
        if (!BuildingManager.instance._canBuild)
            return;
        BuildingManager.instance.BuildTurretOn(this);
    }
 }