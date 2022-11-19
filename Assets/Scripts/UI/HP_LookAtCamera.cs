using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_LookAtCamera : MonoBehaviour
{
    [SerializeField] Transform _camPosition;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(_camPosition.position - transform.position);
        //transform.rotation = new Quaternion(Quaternion.identity.x, 0, 0, Quaternion.identity.w);
    }
}
