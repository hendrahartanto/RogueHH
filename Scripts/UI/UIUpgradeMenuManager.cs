using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeMenuManager : MonoBehaviour
{
  [Header("Upgrade menu panels")]
  [SerializeField] UIUpgradeMenu _upgradeMenuPanel = default;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _startGameplayEvent = default;
  [SerializeField] private VoidEventChannelSO _goToMainMenuEvent = default;

  private void Start()
  {
    SetMenuScreen();
  }

  private void SetMenuScreen()
  {
    _upgradeMenuPanel.StartGameButtonAction += StartGameplay;
  }

  private void StartGameplay()
  {
    _startGameplayEvent.RaiseEvent();
  }

  public void GotoMainMenu()
  {
    _goToMainMenuEvent.RaiseEvent();
  }
}
