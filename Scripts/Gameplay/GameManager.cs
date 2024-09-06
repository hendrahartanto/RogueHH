using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField] private GameStateSO _gameState = default;

  [Header("Listening to")]
  [SerializeField] private GameStateEventChanelSO _changeGameStateEvent = default;

  private void OnEnable()
  {
    _changeGameStateEvent.OnEventRaised += _gameState.SetGameState;
  }

  private void OnDisable()
  {
    _changeGameStateEvent.OnEventRaised -= _gameState.SetGameState;
  }

  private void Start()
  {
    _gameState.SetGameState(GameState.Regular);
  }
}
