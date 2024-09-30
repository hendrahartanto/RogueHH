using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeDetail : MonoBehaviour
{
  public UpgradableItemSO CurrentSelectedItem = default;
  [SerializeField] private GoldSO _goldSO = default;
  private bool _isActive = false;

  [Header("Child component")]
  public GameObject Icon = default;
  public TextMeshProUGUI ItemName = default;
  public TextMeshProUGUI Description = default;
  public GameObject UpgradeButton = default;


  [Header("Broadcasting to")]
  [SerializeField] private IncreaseGlobalPriceEventChannel _increaseGlobalPriceEventBroadcast = default;
  [SerializeField] private UpgradeStatEventChannelSO _upgradeItemEvent = default;
  [SerializeField] private IntEventChanelSO _updateGoldIndicatorUIEvent = default;


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

  public void UpgradeButtonClicked()
  {
    if (!ValidateAndUpdateGold())
      return;

    _upgradeItemEvent.RaiseEvent(CurrentSelectedItem.Type, CurrentSelectedItem.UpgradeValue);

    CurrentSelectedItem.CurrentLevel++;

    //naikin harga global dan harga item sekarang dan sekalian UI item
    _increaseGlobalPriceEventBroadcast.RaiseEvent(CurrentSelectedItem.Type, 10);
    CurrentSelectedItem.Price += 50;

    //update gold indicator UI
    _updateGoldIndicatorUIEvent.RaiseEvent(_goldSO.CurrentGold);

    UpdateUI();
  }

  private bool ValidateAndUpdateGold()
  {
    if (_goldSO.CurrentGold < CurrentSelectedItem.Price)
      return false;

    _goldSO.DecreaseGold(CurrentSelectedItem.Price);

    return true;
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

  private void UpdateUI()
  {
    SetupDescription();
  }

  private void SetActive()
  {
    UpgradeButton.SetActive(true);
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
