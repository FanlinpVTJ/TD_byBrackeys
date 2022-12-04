using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
