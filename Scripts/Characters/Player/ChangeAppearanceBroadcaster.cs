using UnityEngine;

public class ChangeAppearanceBroadcaster : MonoBehaviour
{
  public UpgradableItemSO UpgradableItemSO = default;
  public CharacterConfigSO PlayerConfigSO = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _checkItemLevelEvent = default;

  private void OnEnable()
  {
    _checkItemLevelEvent.OnEventRaised += CheckLevel;
  }

  private void OnDisable()
  {
    _checkItemLevelEvent.OnEventRaised -= CheckLevel;
  }

  private void CheckLevel()
  {
    if (UpgradableItemSO.CurrentLevel >= 20)
    {
      PlayerConfigSO.appearanceType = AppearanceType.Elite;
    }
    else if (UpgradableItemSO.CurrentLevel >= 10)
    {
      PlayerConfigSO.appearanceType = AppearanceType.Normal;
    }
  }
}
