using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField] private GameStateSO _gameState = default;
  [SerializeField] private GoldSO _goldSO = default;
  [SerializeField] private InputReader _inputReader = default;

  [Header("Broadcasting to")]
  [SerializeField] private VoidEventChannelSO _enableGameplayInputEvent = default;
  [SerializeField] private VoidEventChannelSO _playStopBattleMusicEvent = default;

  [Header("Listening to")]
  [SerializeField] private GameStateEventChanelSO _changeGameStateEvent = default;
  [SerializeField] private BoolEventChannelSO _isTurnCyclingSetActiveEvent = default;
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;

  private void OnEnable()
  {
    _changeGameStateEvent.OnEventRaised += SetGameState;
    _onSceneReady.OnEventRaised += SetInitialGameState;
    _isTurnCyclingSetActiveEvent.OnEventRaised += SetIsTurnCycling;
  }

  private void OnDisable()
  {
    _changeGameStateEvent.OnEventRaised -= SetGameState;
    _onSceneReady.OnEventRaised -= SetInitialGameState;
    _isTurnCyclingSetActiveEvent.OnEventRaised -= SetIsTurnCycling;
  }

  private void Start()
  {
    _gameState.SetGameState(GameState.Gameover);
    _gameState.SetIsTurnCycling(false);
  }

  //Event Concrete Action
  private void SetInitialGameState()
  {
    _gameState.SetGameState(GameState.Regular);

    _enableGameplayInputEvent.RaiseEvent();
  }

  private void SetGameState(GameState newGameState)
  {
    if (newGameState == GameState.Combat)
    {
      if (_gameState.CurrentGameState == GameState.Combat)
        return;

      _playStopBattleMusicEvent.RaiseEvent();
    }

    if (newGameState == GameState.Regular && _gameState.CurrentGameState == GameState.Combat)
    {
      _playStopBattleMusicEvent.RaiseEvent();
    }

    if (newGameState == GameState.Alert && _gameState.CurrentGameState == GameState.Alert)
      return;

    if (newGameState == GameState.Gameover)
    {
      _inputReader.DisableAllInput();
      _gameState.SetIsTurnCycling(false);
    }

    _gameState.SetGameState(newGameState);
  }

  private void SetIsTurnCycling(bool isTurnCycling)
  {
    _gameState.SetIsTurnCycling(isTurnCycling);
  }
}
