using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeDetail : MonoBehaviour
{
  public UpgradableItemSO CurrentSelectedItem = default;
  private bool _isActive = false;

  [Header("Child component")]
  public GameObject Icon = default;
  public TextMeshProUGUI ItemName = default;
  public TextMeshProUGUI Description = default;

  [Header("Broadcasting to")]
  [SerializeField] private IncreaseGlobalPriceEventChannel _increaseGlobalPriceEventBroadcast = default;

  [Header("Listening to")]
  [SerializeField] private UpgradableItemSOEventChannelSO _selectUpgradeItemEvent = default;

  private void OnEnable()
  {
    _selectUpgradeItemEvent.OnEventRaised += SetCurrentSelectedItem;
  }

  private void OnDisable()
  {
    _selectUpgradeItemEvent.OnEventRaised -= SetCurrentSelectedItem;
  }

  private void SetCurrentSelectedItem(UpgradableItemSO item)
  {
    CurrentSelectedItem = item;

    if (!_isActive)
      SetActive();

    SetChildComponent();

  }

  private void SetChildComponent()
  {
    Icon.GetComponent<Image>().sprite = CurrentSelectedItem.IconImage;
    ItemName.SetText(CurrentSelectedItem.Name);
    SetupDescription();
  }

  private void SetActive()
  {
    Icon.SetActive(true);
    ItemName.enabled = true;
    Description.enabled = true;
  }

  private void SetupDescription()
  {
    if (CurrentSelectedItem.Type == UpgradeItemType.Health)
    {
      Description.SetText(CurrentSelectedItem.Description + "\nCurrent: " + CurrentSelectedItem.PlayerStats.GetInitialHealth() + " hp\nUpgrade: +" + CurrentSelectedItem.UpgradeValue + " hp\n");
    }
    else if (CurrentSelectedItem.Type == UpgradeItemType.Attack)
    {
    }
    else if (CurrentSelectedItem.Type == UpgradeItemType.Deffend)
    {
    }
    else if (CurrentSelectedItem.Type == UpgradeItemType.CriticalRate)
    {
    }
    else if (CurrentSelectedItem.Type == UpgradeItemType.CriticalDamage)
    {
    }
  }
}
