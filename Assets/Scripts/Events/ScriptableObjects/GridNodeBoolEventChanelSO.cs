using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/GridNodeBoolEventChanel")]
public class GridNodeBoolEventChanelSO : ScriptableObject
{
  public event UnityAction<int, int, bool> OnEventRaised;

  public void RaiseEvent(int x, int z, bool value)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(x, z, value);
  }
}
