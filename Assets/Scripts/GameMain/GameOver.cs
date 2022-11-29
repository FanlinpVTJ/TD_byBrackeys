using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    [SerializeField] WaveSpawner waveSpawner;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI gameOverWavesCountText;
   
    private void Start()
    {
        gameOverUI.SetActive(false);
    }
    
    public void GameEnd()
    {
        gameOverWavesCountText.text = waveSpawner.textWaveIndex.ToString();
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {

    }
}
