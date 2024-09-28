using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeGameStateAction", menuName = "StateMachine/Actions/Gameplay/ChangeGameState")]
public class ChangeGameStateActionSO : StateActionSO
{
  public GameStateSO GameState = default;
  public GameState NewGameState = default;
  public SpecificMoment WhenToRun = default;
  public InputReader InputReader = default;
  protected override StateAction CreateAction() => new ChangeGameStateAction(GameState, NewGameState, WhenToRun, InputReader);
}

public class ChangeGameStateAction : StateAction
{
  private GameState _newGameState = default;
  private GameStateSO _gameState = default;
  private SpecificMoment _whenToRun = default;
  private InputReader _inputReader = default;

  public ChangeGameStateAction(GameStateSO gameState, GameState newGameState, SpecificMoment whenToRun, InputReader inputReader)
  {
    _newGameState = newGameState;
    _gameState = gameState;
    _whenToRun = whenToRun;
    _inputReader = inputReader;
  }

  private void ChangeState()
  {
    if (_newGameState == GameState.Combat)
    {
      if (_gameState.CurrentGameState == GameState.Combat || _gameState.CurrentGameState == GameState.TurnCycling)
        return;
    }

    if (_newGameState == GameState.Gameover)
    {
      _inputReader.DisableAllInput();
    }

    _gameState.SetGameState(_newGameState);
  }

  public override void OnStateEnter()
  {
    if (_whenToRun == SpecificMoment.OnStateEnter)
      ChangeState();
  }

  public override void OnStateExit()
  {
    if (_whenToRun == SpecificMoment.OnStateExit)
      ChangeState();
  }

  public override void OnUpdate() { }
}
