using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Название зачот
public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _startMoney = 400;
    [SerializeField] private int _startLives = 20;

    // TODO: ну я бы от статиков избавился, конечно,
    // Но тогда и изменять эти значения надо не из EnemyMovemet и EnemyHealth
    // а по событиям смерти и достижения конечной точки, это должен делать кто-то другой
    // пусть хотя-бы GameMain это делает, чтоли
    // заодно избавишься от этой логики в Health и Movement, её там быть не должно
    
    // Всё публичное пишется в PascalCase: Wallet, LiveCount
    // публичных полей не должно быть, сделай свойства:
    // public static int Wallet { get; set; }
    // public static int LiveCount { get; set; }
    public static int _wallet; // ну и напоследок, это тогда уж _money, а не Wallet
    public static int _liveCount;
    

    private void Start()
    {
        _wallet = _startMoney;
        _liveCount = _startLives;
    }
}
