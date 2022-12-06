using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    public event Action<bool> OnRetry;

    [SerializeField] private SceneFading sceneFading;

    public void Retry()
    {        
        sceneFading.RunFadeOutTo(SceneManager.GetActiveScene().buildIndex);
        OnRetry.Invoke(false);
    }
}
