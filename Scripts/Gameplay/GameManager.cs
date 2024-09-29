using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField, ReadOnly] private string currentStateName;
  [SerializeField] private GameStateSO _gameState = default;

  [Header("Broadcasting to")]
  [SerializeField] private VoidEventChannelSO _enableGameplayInputEvent = default;
  [SerializeField] private VoidEventChannelSO _disableAllInputEvent = default;

  [Header("Listening to")]
  [SerializeField] private GameStateEventChanelSO _changeGameStateEvent = default;
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;

  private void OnEnable()
  {
    _changeGameStateEvent.OnEventRaised += SetGameState;
    _onSceneReady.OnEventRaised += SetInitialGameState;
  }

  private void OnDisable()
  {
    _changeGameStateEvent.OnEventRaised -= SetGameState;
    _onSceneReady.OnEventRaised -= SetInitialGameState;
  }

  private void Start()
  {
    _gameState.SetGameState(GameState.Regular);

    currentStateName = _gameState.CurrentGameState.ToString();
  }

  //Event Concrete Action
  private void SetInitialGameState()
  {
    _gameState.SetGameState(GameState.Regular);

    currentStateName = _gameState.CurrentGameState.ToString();

    _enableGameplayInputEvent.RaiseEvent();
  }

  private void SetGameState(GameState newGameState)
  {
    if (newGameState == GameState.Gameover)
      _disableAllInputEvent.RaiseEvent();

    _gameState.SetGameState(newGameState);

    currentStateName = _gameState.CurrentGameState.ToString();
  }
}
