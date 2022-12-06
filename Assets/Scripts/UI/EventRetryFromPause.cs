using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventRetryFromPause : MonoBehaviour
{
    public event Action OnRetryFromPause;

    public void RetryFromPause()
    {
        OnRetryFromPause?.Invoke();
    }
}
