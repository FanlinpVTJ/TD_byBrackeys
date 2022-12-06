using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public event Action<bool> OnPause;

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private ContinueButton continueButton;
    [SerializeField] private EventMenuFromPause eventMenuButtonFromPause;
    [SerializeField] private EventRetryFromPause eventRetryButtonFromPause;
    [SerializeField] private SceneFading sceneFading;

    private bool IsFading;

    private void OnEnable()
    {
        sceneFading.OnFading += SetIsFading;
        eventRetryButtonFromPause.OnRetryFromPause += PauseToggle;
        eventMenuButtonFromPause.OnMenuFromPause += PauseToggle;
        continueButton.OnContinue += PauseToggle;
    }

    private void OnDisable()
    {
        sceneFading.OnFading -= SetIsFading;
        eventRetryButtonFromPause.OnRetryFromPause -= PauseToggle;
        eventMenuButtonFromPause.OnMenuFromPause -= PauseToggle;
        continueButton.OnContinue -= PauseToggle;
    }

    private void Start()
    {
        IsFading = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsFading) 
        {
            PauseToggle();
        }
    }

    private void SetIsFading(bool IsFading)
    {
        this.IsFading = IsFading;
    }

    private void PauseToggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        OnPause?.Invoke(pauseUI.activeSelf);
    }
}
