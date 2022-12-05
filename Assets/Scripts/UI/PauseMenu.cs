using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public event Action<bool> OnPause;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private RetryButton retryButton;
    [SerializeField] private MenuButton menuButton;
    [SerializeField] private ContinueButton continueButton;


    private void OnEnable()
    {
        retryButton.OnRetry += PauseToggle;
        menuButton.OnMenu += PauseToggle;
        continueButton.OnContinue += PauseToggle;
    }

    private void OnDisable()
    {
        retryButton.OnRetry -= PauseToggle;
        menuButton.OnMenu -= PauseToggle;
        continueButton.OnContinue -= PauseToggle;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }

    private void PauseToggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        OnPause?.Invoke(pauseUI.activeSelf);
    }
}
