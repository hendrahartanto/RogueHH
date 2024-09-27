using UnityEngine;

public class UIGameplayCanvasManager : MonoBehaviour
{
  private Canvas _gameplayCanvas = default;

  [Header("Gameplay canvas component")]
  [SerializeField] private UIGameOverModal _gameOverModal = default;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _backToUpgradeMenuEvent = default;

  [Header("Listening to")]
  [SerializeField] private BoolEventChannelSO _setGameplayCanvasActiveEvent = default;
  [SerializeField] private BoolEventChannelSO _gameOverModalSetActiveEvent = default;

  private void Awake()
  {
    _gameplayCanvas = GetComponent<Canvas>();
    _gameplayCanvas.enabled = false;
    _gameOverModal.gameObject.SetActive(false);
  }

  private void Start()
  {
    SetGameplayScreen();
  }

  private void SetGameplayScreen()
  {

    _gameOverModal.ContinueButtonAction += BackToUpgradeMenu;
  }

  private void OnDestroy()
  {
    _gameOverModal.ContinueButtonAction -= BackToUpgradeMenu;
  }

  private void OnEnable()
  {
    _setGameplayCanvasActiveEvent.OnEventRaised += SetCanvasActive;
    _gameOverModalSetActiveEvent.OnEventRaised += SetGameOverModalActive;
  }

  private void OnDisable()
  {
    _setGameplayCanvasActiveEvent.OnEventRaised -= SetCanvasActive;
    _gameOverModalSetActiveEvent.OnEventRaised -= SetGameOverModalActive;
  }

  public void SetCanvasActive(bool isActive)
  {
    _gameplayCanvas.enabled = isActive;
  }

  public void SetGameOverModalActive(bool isActive)
  {
    _gameOverModal.gameObject.SetActive(isActive);
  }

  private void BackToUpgradeMenu()
  {
    _backToUpgradeMenuEvent.RaiseEvent();
    _gameOverModal.gameObject.SetActive(false);
  }
}
