using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/IncreaseGlobalPriceEventChannel")]
public class IncreaseGlobalPriceEventChannel : ScriptableObject
{
  public event UnityAction<UpgradeItemType, int> OnEventRaised;

  public void RaiseEvent(UpgradeItemType type, int priceIncreaseValue)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(type, priceIncreaseValue);
  }
}
