using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
  private bool _hasSaveData = false;

  [Header("Mainmenu component")]
  [SerializeField] UIMainMenu _mainMenuPanel = default;
  [SerializeField] UIAlertModal _alertModal = default;
  [SerializeField] VoidEventChannelSO _onSceneReady = default;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _startNewGameEvent = default;
  [SerializeField] private VoidEventChannelSO _resetSaveableDataEvent = default;
  [SerializeField] private VoidEventChannelSO _loadSaveableDataEvent = default;
  [SerializeField] private PlaySFXEventChannelSO _playSFXEvent = default;

  private void Awake()
  {
    _hasSaveData = SaveSystem.CheckHasSaveData();
  }

  private void OnEnable()
  {
    _onSceneReady.OnEventRaised += PlayCampfireSFX;
  }

  private void OnDisable()
  {
    _onSceneReady.OnEventRaised -= PlayCampfireSFX;
  }

  private void Start()
  {
    SetMenuScreen();
    SetAlertModal();
  }

  private void SetAlertModal()
  {
    _alertModal.BackButtonAction += ToggleAlertModal;
    _alertModal.ContinueButtonAction += StartNewGame;
  }

  private void SetMenuScreen()
  {
    if (!_hasSaveData)
    {
      _mainMenuPanel.NewGameButtonAction += StartNewGame;
      _mainMenuPanel.ContinueButtonComp.DisableButton();
    }
    else
    {
      _mainMenuPanel.NewGameButtonAction += ToggleAlertModal;
      _mainMenuPanel.ContinueButtonAction += LoadGame;
    }
  }

  private void OnDestroy()
  {
    if (!_hasSaveData)
      _mainMenuPanel.NewGameButtonAction -= StartNewGame;
    else
    {
      _mainMenuPanel.NewGameButtonAction -= ToggleAlertModal;
      _mainMenuPanel.ContinueButtonAction -= LoadGame;
    }

    //alert modal
    _alertModal.BackButtonAction -= ToggleAlertModal;
    _alertModal.ContinueButtonAction += StartNewGame;

    _playSFXEvent.RaiseStopEvent(SFXName.Campfire);
  }

  private void StartNewGame()
  {
    _resetSaveableDataEvent.RaiseEvent();
    _startNewGameEvent.RaiseEvent();
  }

  private void LoadGame()
  {
    _loadSaveableDataEvent.RaiseEvent();
    _startNewGameEvent.RaiseEvent();
  }

  private void ToggleAlertModal()
  {
    _alertModal.gameObject.SetActive(!_alertModal.gameObject.activeSelf);
  }

  private void PlayCampfireSFX() => _playSFXEvent.RaiseEvent(SFXName.Campfire, transform);

}
