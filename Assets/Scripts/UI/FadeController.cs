using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
  [SerializeField] private FadeEventChannelSO _fadeEvent = default;
  [SerializeField] private Image _imageComponent;

  private void OnEnable()
  {
    _fadeEvent.OnEventRaised += StartFade;
  }

  private void OnDisable()
  {
    _fadeEvent.OnEventRaised -= StartFade;
  }

  private void StartFade(bool fadeIn, float duration)
  {
    StartCoroutine(FadeImage(fadeIn, duration));
  }

  private IEnumerator FadeImage(bool fadeIn, float duration)
  {
    float startAlpha = fadeIn ? 0f : 1f;
    float endAlpha = fadeIn ? 1f : 0f;
    float elapsedTime = 0f;

    Color imageColor = _imageComponent.color;
    imageColor.a = startAlpha;
    _imageComponent.color = imageColor;

    while (elapsedTime < duration)
    {
      elapsedTime += Time.deltaTime;
      float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
      imageColor.a = newAlpha;
      _imageComponent.color = imageColor;

      yield return null;
    }

    imageColor.a = endAlpha;
    _imageComponent.color = imageColor;
  }
}
