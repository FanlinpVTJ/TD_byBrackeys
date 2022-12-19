using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public event Action<ShopItem> OnClicked;

    [SerializeField] private TMP_Text cost;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;
    
    public TurretConfig Config { get; private set; }

    private void Awake()
    {
        button.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        OnClicked?.Invoke(this);
    }

    public void Initialize(TurretConfig config)
    {
        Config = config;
        cost.text = $"${config.Cost}";
        icon.sprite = config.IconSprite;
    }
}
