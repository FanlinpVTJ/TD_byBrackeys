using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFading : MonoBehaviour
{
    public event Action<bool> OnFading;

    [SerializeField] private Image fadingImage;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float time;

    private float fadingTime;

    private void Start()
    {
        fadingTime = time;
        StartCoroutine(FadeIn());
    }

    public void RunFadeOutTo(int sceneIndex)
    {
        StartCoroutine(FadeOut(sceneIndex));
    }

    private IEnumerator FadeIn()
    {
        OnFading?.Invoke(false);
        while (fadingTime > 0)
        {
            var alfaChannel = animationCurve.Evaluate(fadingTime);
            fadingImage.color = new Color(0f, 0f, 0f, alfaChannel);
            fadingTime -= Time.deltaTime;
            yield return null;
        }
        OnFading?.Invoke(true);
    }

    private IEnumerator FadeOut(int sceneIndex)
    {
        OnFading?.Invoke(false);
        while (fadingTime < 1)
        {
            var alfaChannel = animationCurve.Evaluate(fadingTime);
            fadingImage.color = new Color(0f, 0f, 0f, alfaChannel);
            fadingTime += Time.deltaTime;
            yield return null;
        }
        OnFading?.Invoke(true);
        SceneManager.LoadScene(sceneIndex);
    }
}
