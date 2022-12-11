using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    public static List<UnitHealthSystem> ListOfEnemies { get; private set; }


    private void Awake()
    {
        ListOfEnemies = new List<UnitHealthSystem>();
    }

    public void AddToList(UnitHealthSystem unitHealthSystem)
    {
        ListOfEnemies.Add(unitHealthSystem);
        unitHealthSystem.OnDeath += RemoveFromList;
    }
    
    public void RemoveFromList(UnitHealthSystem unitHealthSystem)
    {
        ListOfEnemies.Remove(unitHealthSystem);
        unitHealthSystem.OnDeath -= RemoveFromList;
    }
}
