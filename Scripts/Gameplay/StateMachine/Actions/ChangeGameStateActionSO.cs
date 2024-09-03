using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeGameStateAction", menuName = "StateMachine/Actions/Gameplay/ChangeGameState")]
public class ChangeGameStateActionSO : StateActionSO
{
  public GameStateSO GameState = default;
  public GameState NewGameState = default;
  protected override StateAction CreateAction() => new ChangeGameStateAction(GameState, NewGameState);
}

public class ChangeGameStateAction : StateAction
{
  private GameState _newGameState = default;
  private GameStateSO _gameState = default;

  public ChangeGameStateAction(GameStateSO gameState, GameState newGameState)
  {
    _newGameState = newGameState;
    _gameState = gameState;
  }

  private void ChangeState()
  {
    if (_newGameState == GameState.Combat)
    {
      if (_gameState.CurrentGameState == GameState.Combat || _gameState.CurrentGameState == GameState.TurnCycling)
        return;
    }
    _gameState.SetGameState(_newGameState);
  }

  public override void OnStateEnter()
  {
    ChangeState();
  }

  public override void OnStateExit()
  {
  }

  public override void OnUpdate() { }
}
