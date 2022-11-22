using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TEXT_Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentPlayerMoney;


    // Update is called once per frame
    void Update()
    {
        _currentPlayerMoney.text = "$" + PlayerStats.wallet.ToString();
    }
}
