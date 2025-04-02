using TMPro;
using UnityEngine;

public class UIPlayerLevelIndicator : MonoBehaviour
{
  [SerializeField] private CharacterConfigSO _characterConfigSO = default;
  [SerializeField] private TextMeshProUGUI _levelText = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _playerLevelUpEvent = default;
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;

  private void OnEnable()
  {
    _playerLevelUpEvent.OnEventRaised += SetupText;
    _onSceneReady.OnEventRaised += SetupText;
  }

  private void OnDisable()
  {
    _playerLevelUpEvent.OnEventRaised -= SetupText;
    _onSceneReady.OnEventRaised -= SetupText;
  }

  private void Start()
  {
    SetupText();
  }

  private void SetupText()
  {
    _levelText.SetText("Level" + (_characterConfigSO.Level + 1));
  }

}
