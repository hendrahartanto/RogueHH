using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/TextPopupEventChannel")]
public class TextPopupEventChannelSO : ScriptableObject
{
  public event UnityAction<Vector3, String, TextColor, float> OnEventRaised;
  public void RaiseEvent(Vector3 pos, String text, TextColor color, float yOffset = 0)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(pos, text, color, yOffset);
  }
}
