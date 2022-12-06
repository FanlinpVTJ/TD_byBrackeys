using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneFading sceneFading;
    public void StartGame()
    {
        sceneFading.RunFadeOutTo(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
