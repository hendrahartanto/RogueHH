using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
  [Header("Mainmenu component")]
  [SerializeField] UIMainMenu _mainMenuPanel = default;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _startNewGameEvent = default;

  private void Start()
  {
    SetMenuScreen();
  }

  private void SetMenuScreen()
  {
    _mainMenuPanel.NewGameButtonAction += StartNewGame;
  }

  private void OnDestroy()
  {
    _mainMenuPanel.NewGameButtonAction -= StartNewGame;
  }

  private void StartNewGame()
  {
    _startNewGameEvent.RaiseEvent();
  }


}
