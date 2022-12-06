using System;
using UnityEngine;

public class EventMenuFromPause : MonoBehaviour
{
    public event Action OnMenuFromPause;

    public void ShowMenuFromPause()
    {
        OnMenuFromPause?.Invoke();
    }
}
