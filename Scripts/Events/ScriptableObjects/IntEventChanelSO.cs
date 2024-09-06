using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/IntEventChanel")]
public class IntEventChanelSO : ScriptableObject
{
  public event UnityAction<int> OnEventRaised;

  public void RaiseEvent(int value)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(value);
  }
}
