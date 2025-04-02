using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsInSpecificGameState", menuName = "StateMachine/Conditions/Gameplay/IsInSpecificGameState")]
public class IsInSpecificGameStateConditionSO : StateConditionSO
{
  public GameState GameStateToCheck;
  public GameStateSO GameState;

  protected override Condition CreateCondition() => new IsInSpecificGameStateCondition(GameStateToCheck, GameState);
}

public class IsInSpecificGameStateCondition : Condition
{
  private GameState _gameStateToCheck = default;
  private GameStateSO _gameState = default;
  public IsInSpecificGameStateCondition(GameState gameStateToCheck, GameStateSO gameState)
  {
    _gameState = gameState;
    _gameStateToCheck = gameStateToCheck;
  }

  protected override bool Statement()
  {
    return _gameStateToCheck == _gameState.CurrentGameState;
  }
}
