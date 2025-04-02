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
  public GameObject CostIndicator = default;
  public TextMeshProUGUI ErrorMessage = default;


  [Header("Broadcasting to")]
  [SerializeField] private IncreaseGlobalPriceEventChannel _increaseGlobalPriceEventBroadcast = default;
  [SerializeField] private UpgradeStatEventChannelSO _upgradeItemEvent = default;
  [SerializeField] private IntEventChanelSO _updateGoldIndicatorEvent = default;
  [SerializeField] private PlaySFXEventChannelSO _playSFXEvent = default;
  [SerializeField] private VoidEventChannelSO _checkItemLevelEvent = default;


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

    CurrentSelectedItem.IncrementLevel();

    _checkItemLevelEvent.RaiseEvent();

    //naikin harga global dan harga item sekarang dan sekalian UI item
    _increaseGlobalPriceEventBroadcast.RaiseEvent(CurrentSelectedItem.Type, 10);
    CurrentSelectedItem.IncreasePrice(50);

    //update gold indicator UI
    _updateGoldIndicatorEvent.RaiseEvent(_goldSO.CurrentGold);

    UpdateUI();

    PlaySFXPurchase();
  }

  private bool ValidateAndUpdateGold()
  {
    if (_goldSO.CurrentGold < CurrentSelectedItem.Price)
    {
      ErrorMessage.enabled = true;
      return false;
    }

    _goldSO.DecreaseGold(CurrentSelectedItem.Price);

    return true;
  }

  private void IsReachedMaxLevel()
  {
    if (CurrentSelectedItem.MaxLevel != 0 && CurrentSelectedItem.CurrentLevel >= CurrentSelectedItem.MaxLevel)
      UpgradeButton.SetActive(false);
  }

  private void SetCurrentSelectedItem(UpgradableItemSO item)
  {
    CurrentSelectedItem = item;

    if (!_isActive)
      SetActive(item);

    SetChildComponent();

  }

  private void SetChildComponent()
  {
    Icon.GetComponent<Image>().sprite = CurrentSelectedItem.IconImage;
    ItemName.SetText(CurrentSelectedItem.Name);
    SetupDescription();
    CostIndicator.GetComponentInChildren<TextMeshProUGUI>().SetText(CurrentSelectedItem.Price + " To upgrade");
  }

  private void UpdateUI()
  {
    SetupDescription();
    IsReachedMaxLevel();
    CostIndicator.GetComponentInChildren<TextMeshProUGUI>().SetText(CurrentSelectedItem.Price + " To upgrade");
  }

  private void SetActive(UpgradableItemSO item)
  {
    if (item.CurrentLevel < item.MaxLevel)
      UpgradeButton.SetActive(true);

    Icon.SetActive(true);
    ItemName.enabled = true;
    Description.enabled = true;
    CostIndicator.SetActive(true);
    ErrorMessage.enabled = false;
  }

  private void SetupDescription()
  {
    if (CurrentSelectedItem.Type == UpgradeItemType.Health)
    {
      Description.SetText(CurrentSelectedItem.Description + "\n\nCurrent: " + CurrentSelectedItem.PlayerStats.GetInitialHealth() + " hp\nUpgrade: +" + CurrentSelectedItem.UpgradeValue + " hp\n");
    }
    else if (CurrentSelectedItem.Type == UpgradeItemType.Attack)
    {
      Description.SetText(CurrentSelectedItem.Description + "\n\nCurrent: " + CurrentSelectedItem.PlayerStats.GetInitialAttackPoint() + " atk\nUpgrade: +" + CurrentSelectedItem.UpgradeValue + " atk\n");
    }
    else if (CurrentSelectedItem.Type == UpgradeItemType.Deffend)
    {
      Description.SetText(CurrentSelectedItem.Description + "\n\nCurrent: " + CurrentSelectedItem.PlayerStats.GetInitialDeffendPoint() + " def\nUpgrade: +" + CurrentSelectedItem.UpgradeValue + " def\n");
    }
    else if (CurrentSelectedItem.Type == UpgradeItemType.CriticalRate)
    {
      Description.SetText(CurrentSelectedItem.Description + "\n\nCurrent: " + CurrentSelectedItem.PlayerStats.CriticalRate * 100 + "% rate\nUpgrade: +" + CurrentSelectedItem.UpgradeValue * 100 + "% rate\n");
    }
    else if (CurrentSelectedItem.Type == UpgradeItemType.CriticalDamage)
    {
      Description.SetText(CurrentSelectedItem.Description + "\n\nCurrent: " + CurrentSelectedItem.PlayerStats.CriticalDamage * 100 + "% dmg\nUpgrade: +" + CurrentSelectedItem.UpgradeValue * 100 + "% dmg\n");
    }
  }

  private void PlaySFXPurchase()
  {
    _playSFXEvent.RaiseEvent(SFXName.Purchase, transform);
  }
}
