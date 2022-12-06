using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public event Action<bool> OnMenu;

    [SerializeField] private SceneFading sceneFading;

    public void ShowMenu()
    {
        sceneFading.RunFadeOutTo(0);
        OnMenu.Invoke(false);
    }
}
