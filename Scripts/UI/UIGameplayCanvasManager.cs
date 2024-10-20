using UnityEngine;

public class UIGameplayCanvasManager : MonoBehaviour
{
  private Canvas _gameplayCanvas = default;
  [SerializeField] private InputReader _inputReader = default;
  [SerializeField] private DungeonSO _dungeonSO = default;
  public GameObject GotoNextFloorController = default;
  public GameObject GotoUpgradeMenuController = default;

  [Header("Gameplay canvas component")]
  [SerializeField] private UIGameOverModal _gameOverModal = default;
  [SerializeField] private UIFloorClearedModal _floorClearedModal = default;
  [SerializeField] private UIPauseModal _pauseModal = default;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _backToUpgradeMenuEvent = default;
  [SerializeField] private VoidEventChannelSO _startGameplayEvent = default;
  [SerializeField] private VoidEventChannelSO _goToMainMenuEvent = default;
  [SerializeField] private VoidEventChannelSO _toggleRaycastActiveEvent = default;


  [Header("Listening to")]
  [SerializeField] private BoolEventChannelSO _setGameplayCanvasActiveEvent = default;
  [SerializeField] private BoolEventChannelSO _gameOverModalSetActiveEvent = default;
  [SerializeField] private BoolEventChannelSO _floorClearedModalSetActiveEvent = default;
  [SerializeField] private VoidEventChannelSO _pauseModalToggleEvent = default;
  [SerializeField] private PlaySFXEventChannelSO _playSFXEvent = default;

  private void Awake()
  {
    _gameplayCanvas = GetComponent<Canvas>();
    _gameplayCanvas.enabled = false;

    _gameOverModal.gameObject.SetActive(false);
    _floorClearedModal.gameObject.SetActive(false);
    _pauseModal.gameObject.SetActive(false);
  }

  private void Start()
  {
    SetGameplayScreen();

  }

  private void SetGameplayScreen()
  {
    _gameOverModal.ContinueButtonAction += GoToSpecificMenu;

    _floorClearedModal.ButtonAction += GoToSpecificMenu;

    _pauseModal.ResumeButtonAction += TogglePauseModal;
    _pauseModal.BackToUpgradeButtonAction += GoToSpecificMenu;
    _pauseModal.ExitToMainMenuButtonAction += GotoMainMenu;
  }

  private void OnDestroy()
  {
    _gameOverModal.ContinueButtonAction -= GoToSpecificMenu;

    _floorClearedModal.ButtonAction -= GoToSpecificMenu;

    _pauseModal.ResumeButtonAction -= TogglePauseModal;
    _pauseModal.BackToUpgradeButtonAction -= GoToSpecificMenu;
    _pauseModal.ExitToMainMenuButtonAction -= GotoMainMenu;
  }

  private void OnEnable()
  {
    _setGameplayCanvasActiveEvent.OnEventRaised += SetCanvasActive;

    _gameOverModalSetActiveEvent.OnEventRaised += SetGameOverModalActive;
    _floorClearedModalSetActiveEvent.OnEventRaised += SetFloorClearedModalActive;
    _pauseModalToggleEvent.OnEventRaised += TogglePauseModal;
  }

  private void OnDisable()
  {
    _setGameplayCanvasActiveEvent.OnEventRaised -= SetCanvasActive;

    _gameOverModalSetActiveEvent.OnEventRaised -= SetGameOverModalActive;
    _floorClearedModalSetActiveEvent.OnEventRaised += SetFloorClearedModalActive;
    _pauseModalToggleEvent.OnEventRaised -= TogglePauseModal;
  }

  public void SetCanvasActive(bool isActive)
  {
    _gameplayCanvas.enabled = isActive;
  }

  public void SetGameOverModalActive(bool isActive)
  {
    _gameOverModal.gameObject.SetActive(isActive);
  }

  public void SetFloorClearedModalActive(bool isActive)
  {
    _floorClearedModal.gameObject.SetActive(isActive);

    if (_dungeonSO.CurrentLevel == -1)
      GotoNextFloorController.SetActive(false);
    else
      GotoUpgradeMenuController.SetActive(false);
  }

  private void GoToSpecificMenu()
  {
    _backToUpgradeMenuEvent.RaiseEvent();
    _startGameplayEvent.RaiseEvent();

    _gameOverModal.gameObject.SetActive(false);
    _floorClearedModal.gameObject.SetActive(false);
    _pauseModal.gameObject.SetActive(false);
  }

  private void GotoMainMenu()
  {
    _goToMainMenuEvent.RaiseEvent();

    _pauseModal.gameObject.SetActive(false);
  }

  private void TogglePauseModal()
  {
    if (!_pauseModal.gameObject.activeSelf)
      _playSFXEvent.RaiseEvent(SFXName.MenuOpen, transform);
    else
      _playSFXEvent.RaiseEvent(SFXName.MenuClosed, transform);

    _inputReader.ToggleGameplayInput();
    _pauseModal.gameObject.SetActive(!_pauseModal.gameObject.activeSelf);
    _toggleRaycastActiveEvent.RaiseEvent();
  }
}
