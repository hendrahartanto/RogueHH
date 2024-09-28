using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
  }

  //Event Concrete Action
  private void SetInitialGameState()
  {
    _gameState.SetGameState(GameState.Regular);
    _enableGameplayInputEvent.RaiseEvent();
  }

  private void SetGameState(GameState newGameState)
  {
    if (newGameState == GameState.Gameover)
      _disableAllInputEvent.RaiseEvent();

    _gameState.SetGameState(newGameState);
  }
}
