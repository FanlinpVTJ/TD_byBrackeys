using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public event Action OnMenu;

    [SerializeField] private SceneFading sceneFading;

    public void ShowMenu()
    {
        SceneManager.LoadScene(0);
        OnMenu?.Invoke();
    }
}
