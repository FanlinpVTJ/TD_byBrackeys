using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TEXT_Lives : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentPlayerLives;

    // Update is called once per frame
    void Update()
    {
        _currentPlayerLives.text = PlayerStats._liveCount.ToString();
    }
}
