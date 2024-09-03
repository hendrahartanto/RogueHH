using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/TurnComponentEventChanel")]
public class TurnComponentEventChanelSO : ScriptableObject
{
  public event UnityAction<ITurnComponent> OnEventRaised;
  public void RaiseEvent(ITurnComponent value)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(value);
  }
}
