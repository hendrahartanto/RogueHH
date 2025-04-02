using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/FadeEventChannel")]
public class FadeEventChannelSO : ScriptableObject
{
  public UnityAction<bool, float> OnEventRaised;

  public void FadeIn(float duration)
  {
    Fade(true, duration);
  }

  public void FadeOut(float duration)
  {
    Fade(false, duration);
  }

  private void Fade(bool fadeIn, float duration)
  {
    if (OnEventRaised != null)
    {
      OnEventRaised.Invoke(fadeIn, duration);
    }
  }
}
