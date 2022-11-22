using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: вот это я вообще не понял, зачем надо))
// название тоже ни о чем не говорит
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
