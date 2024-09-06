using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/ChangeCellTypeEventChanel")]
public class ChangeCellTypeEventChanel : ScriptableObject
{
  public event UnityAction<int, int, CellType> OnEventRaised;

  public void RaiseEvent(int x, int z, CellType cellType)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(x, z, cellType);
  }
}
