using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/PlaySFXEventChannel")]
public class PlaySFXEventChannelSO : ScriptableObject
{
  public event UnityAction<SFXName, Transform> OnEventRaised;

  public void RaiseEvent(SFXName sfxName, Transform transform)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(sfxName, transform);
  }
}
