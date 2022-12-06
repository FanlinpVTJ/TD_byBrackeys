using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLevelSelector : MonoBehaviour
{
    [SerializeField] private SceneFading sceneFading;
    [SerializeField] Button[] buttons;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i+1 > levelReached)
                buttons[i].interactable = false;
        }
    }

    public void LoadSelectScene(int sceneIndex)
    {
        sceneFading.RunFadeOutTo(sceneIndex);
    }
}
