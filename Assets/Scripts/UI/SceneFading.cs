using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFading : MonoBehaviour
{
    [SerializeField] private Image fadingImage;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float fadingSpeed;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void RunFadeOutTo(int sceneIndex)
    {
        StartCoroutine(FadeOut(sceneIndex));
    }

    private IEnumerator FadeIn()
    {
        var time = 1f;
        while(time > 0)
        {
            var alfaChannel = animationCurve.Evaluate(time);
            fadingImage.color = new Color(0f, 0f, 0f, alfaChannel);
            time -= Time.deltaTime * fadingSpeed;
            yield return null;
        }
    }

    private IEnumerator FadeOut(int sceneIndex)
    {
        var time = 0f;
        while (time < 1)
        {
            var alfaChannel = animationCurve.Evaluate(time);
            fadingImage.color = new Color(0f, 0f, 0f, alfaChannel);
            time += Time.deltaTime * fadingSpeed;
            yield return null;
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
