using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRegistration : MonoBehaviour
{
    private void OnTriggerEnter(Collider _bullet)
    {
        if (_bullet.tag == "Bullet")
        {
           // Debug.Log("Popal");
        }
    }
}
