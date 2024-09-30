using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
  [SerializeField] private CharacterConfigSO _playerConfigSO = default;

  [Header("Listening to")]
  [SerializeField] private UpgradeStatEventChannelSO _upgradeItemEvent = default;

  private void OnEnable()
  {
    _upgradeItemEvent.OnEventRaised += UpgradeItem;
  }

  private void OnDisable()
  {
    _upgradeItemEvent.OnEventRaised -= UpgradeItem;
  }

  private void UpgradeItem(UpgradeItemType type, float value)
  {
    if (type == UpgradeItemType.Health)
    {
      _playerConfigSO.MinInitialHealth += (int)value;
      _playerConfigSO.MaxInitialHealth += (int)value;
    }
    else if (type == UpgradeItemType.Attack)
    {
      _playerConfigSO.MinInitialAttackPoint += (int)value;
      _playerConfigSO.MaxInitialAttackPoint += (int)value;
    }
    else if (type == UpgradeItemType.Deffend)
    {
      _playerConfigSO.MinInitialDeffendPoint += (int)value;
      _playerConfigSO.MaxInitialDeffendPoint += (int)value;
    }
    else if (type == UpgradeItemType.CriticalRate)
    {
      _playerConfigSO.CriticalRate += value;
      _playerConfigSO.CriticalRate = Mathf.Round(_playerConfigSO.CriticalRate * 1000f) / 1000f;
    }
    else if (type == UpgradeItemType.CriticalDamage)
    {
      _playerConfigSO.CriticalDamage += value;
      _playerConfigSO.CriticalDamage = Mathf.Round(_playerConfigSO.CriticalDamage * 1000f) / 1000f;
    }
  }
}
