using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

// TODO: наверное это TurretBuilder или TurretBuildInput или типа того
public class NodeSelectionBuild : MonoBehaviour
{
    [SerializeField] private Color _hoverColor;
    [SerializeField] private Color _notEnoughtMoneyColor;

    [Header("Optional")]
    public GameObject _turret; // TODO: публичное PascalCase, сделать свойство { get; set; }, почему Optional?
    private Color _startColor;
    private Renderer _renderer;

    // TODO: private забыл
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
        if (!BuildingManager.instance._canBuild)
            return;
        if (_turret != null)
        {
            Destroy(_turret.gameObject);
            return;
        }
        BuildingManager.instance.BuildTurretOn(this);
    }
 }