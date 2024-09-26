using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/DamagePopupEventChannel")]
public class DamagePopupEventChannel : ScriptableObject
{
  public event UnityAction<Vector3, int, bool> OnEventRaised;
  public void RaiseEvent(Vector3 pos, int value, bool critical = false)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(pos, value, critical);
  }
}
