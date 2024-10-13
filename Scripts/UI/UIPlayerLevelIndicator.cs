using TMPro;
using UnityEngine;

public class UIPlayerLevelIndicator : MonoBehaviour
{
  [SerializeField] private CharacterConfigSO _characterConfigSO = default;
  [SerializeField] private TextMeshProUGUI _levelText = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _playerLevelUpEvent = default;

  private void OnEnable()
  {
    _playerLevelUpEvent.OnEventRaised += SetupText;
  }

  private void OnDisable()
  {
    _playerLevelUpEvent.OnEventRaised -= SetupText;
  }

  private void Awake()
  {
    SetupText();
  }

  private void SetupText()
  {
    _levelText.SetText("Level" + (_characterConfigSO.Level + 1));
  }

}
