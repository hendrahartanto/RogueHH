using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeGameStateAction", menuName = "StateMachine/Actions/Gameplay/ChangeGameState")]
public class ChangeGameStateActionSO : StateActionSO
{
  public GameState NewGameState = default;
  public SpecificMoment WhenToRun = default;
  public GameStateEventChanelSO ChangeGameStateEvent = default;

  protected override StateAction CreateAction() => new ChangeGameStateAction(NewGameState, WhenToRun, ChangeGameStateEvent);
}

public class ChangeGameStateAction : StateAction
{
  private GameState _newGameState = default;
  private SpecificMoment _whenToRun = default;
  private GameStateEventChanelSO _changeGameStateEvent = default;

  public ChangeGameStateAction(GameState newGameState, SpecificMoment whenToRun, GameStateEventChanelSO changeGameStateEvent)
  {
    _newGameState = newGameState;
    _whenToRun = whenToRun;
    _changeGameStateEvent = changeGameStateEvent;
  }

  private void ChangeState()
  {
    _changeGameStateEvent.RaiseEvent(_newGameState);
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
