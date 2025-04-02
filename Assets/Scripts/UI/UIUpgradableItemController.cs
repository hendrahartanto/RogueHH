using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradableItemController : MonoBehaviour
{
  [SerializeField] private UpgradableItemSO _upgradableItemSO = default;
  [SerializeField] private GameObject _icon = default;
  [SerializeField] private TextMeshProUGUI _levelText = default;

  [Header("Broadcasting to")]
  [SerializeField] private UpgradableItemSOEventChannelSO _selectUpgradeItemEvent = default;

  [Header("Listening to")]
  [SerializeField] private IncreaseGlobalPriceEventChannel _increaseGlobalPriceEventListen = default;
  [SerializeField] private UpgradeStatEventChannelSO _upgradeItemEvent = default;

  private void Awake()
  {
    SetupComponent();
  }

  private void OnEnable()
  {
    _increaseGlobalPriceEventListen.OnEventRaised += IncreaseItemPrice;
  }

  private void OnDisable()
  {
    _increaseGlobalPriceEventListen.OnEventRaised -= IncreaseItemPrice;
  }

  public void OnItemClicked()
  {
    _selectUpgradeItemEvent.RaiseEvent(_upgradableItemSO);
  }

  private void IncreaseItemPrice(UpgradeItemType type, int value)
  {
    if (_upgradableItemSO.Type == type)
    {
      UpdateUI();
      return;
    }

    _upgradableItemSO.IncreasePrice(value);

  }

  private void SetupComponent()
  {
    _icon.GetComponent<Image>().sprite = _upgradableItemSO.IconImage;

    if (_upgradableItemSO.MaxLevel != 0)
      _levelText.SetText("Lvl." + _upgradableItemSO.CurrentLevel + "/" + _upgradableItemSO.MaxLevel);
    else
      _levelText.SetText("Lvl." + _upgradableItemSO.CurrentLevel);
  }

  private void UpdateUI()
  {
    if (_upgradableItemSO.MaxLevel != 0)
      _levelText.SetText("Lvl." + _upgradableItemSO.CurrentLevel + "/" + _upgradableItemSO.MaxLevel);
    else
      _levelText.SetText("Lvl." + _upgradableItemSO.CurrentLevel);
  }
}
