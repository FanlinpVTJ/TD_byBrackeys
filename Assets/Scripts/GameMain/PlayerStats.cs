using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _startMoney = 400;
    [SerializeField] private int _startLives = 20;

    public static int _wallet;
    public static int _liveCount;
    

    private void Start()
    {
        _wallet = _startMoney;
        _liveCount = _startLives;
    }
}
