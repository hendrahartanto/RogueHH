using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/UpgradeStatEventChannel")]
public class UpgradeStatEventChannelSO : ScriptableObject
{
  public event UnityAction<UpgradeItemType, float> OnEventRaised;

  public void RaiseEvent(UpgradeItemType upgradeType, float value)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(upgradeType, value);
  }
}

public enum UpgradeItemType
{
  Health,
  Deffend,
  Attack,
  CriticalRate,
  CriticalDamage
}
