using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField] private GameStateSO _gameState = default;

  [Header("Listening to")]
  [SerializeField] private GameStateEventChanelSO _turnCycleEvent = default;

  private void OnEnable()
  {
    _turnCycleEvent.OnEventRaised += _gameState.SetGameState;
  }

  private void OnDisable()
  {
    _turnCycleEvent.OnEventRaised -= _gameState.SetGameState;
  }

  private void Start()
  {
    _gameState.SetGameState(GameState.Regular);
  }
}
