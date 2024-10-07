using UnityEngine;

public class UIGameplayCanvasManager : MonoBehaviour
{
  private Canvas _gameplayCanvas = default;

  [Header("Gameplay canvas component")]
  [SerializeField] private UIGameOverModal _gameOverModal = default;
  [SerializeField] private UIFloorClearedModal _floorClearedModal = default;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _backToUpgradeMenuEvent = default;
  [SerializeField] private VoidEventChannelSO _startGameplayEvent = default;

  [Header("Listening to")]
  [SerializeField] private BoolEventChannelSO _setGameplayCanvasActiveEvent = default;
  [SerializeField] private BoolEventChannelSO _gameOverModalSetActiveEvent = default;
  [SerializeField] private BoolEventChannelSO _floorClearedModalSetActiveEvent = default;

  private void Awake()
  {
    _gameplayCanvas = GetComponent<Canvas>();
    _gameplayCanvas.enabled = false;

    _gameOverModal.gameObject.SetActive(false);
    _floorClearedModal.gameObject.SetActive(false);
  }

  private void Start()
  {
    SetGameplayScreen();
  }

  private void SetGameplayScreen()
  {
    _gameOverModal.ContinueButtonAction += GoToSpecificMenu;
    _floorClearedModal.ButtonAction += GoToSpecificMenu;
  }

  private void OnDestroy()
  {
    _gameOverModal.ContinueButtonAction -= GoToSpecificMenu;
    _floorClearedModal.ButtonAction -= GoToSpecificMenu;
  }

  private void OnEnable()
  {
    _setGameplayCanvasActiveEvent.OnEventRaised += SetCanvasActive;

    _gameOverModalSetActiveEvent.OnEventRaised += SetGameOverModalActive;
    _floorClearedModalSetActiveEvent.OnEventRaised += SetFloorClearedModalActive;
  }

  private void OnDisable()
  {
    _setGameplayCanvasActiveEvent.OnEventRaised -= SetCanvasActive;

    _gameOverModalSetActiveEvent.OnEventRaised -= SetGameOverModalActive;
    _floorClearedModalSetActiveEvent.OnEventRaised += SetFloorClearedModalActive;
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
  }

  private void GoToSpecificMenu()
  {
    _backToUpgradeMenuEvent.RaiseEvent();
    _startGameplayEvent.RaiseEvent();

    _gameOverModal.gameObject.SetActive(false);
    _floorClearedModal.gameObject.SetActive(false);
  }
}
