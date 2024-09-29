using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/UpgradableItemSOEventChannel")]
public class UpgradableItemSOEventChannelSO : ScriptableObject
{
  public event UnityAction<UpgradableItemSO> OnEventRaised;

  public void RaiseEvent(UpgradableItemSO SO)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(SO);
  }
}
