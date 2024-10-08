using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/IntIntEventChanel")]
public class IntIntEventChannelSO : ScriptableObject
{
  public event UnityAction<int, int> OnEventRaised;

  public void RaiseEvent(int value, int value2)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(value, value2);
  }
}
