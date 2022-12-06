using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    public event Action OnRetry;

    [SerializeField] private SceneFading sceneFading;

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        OnRetry?.Invoke();
    }
}
