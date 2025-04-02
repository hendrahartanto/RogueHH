using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StopPlayerMovementOnClickAction", menuName = "StateMachine/Actions/Pointer/StopPlayerMovementOnClickAction")]
public class StopPlayerMovementOnClickActionSO : StateActionSO
{
  public InputReader InputReader = default;
  protected override StateAction CreateAction() => new StopPlayerMovementOnClickAction(InputReader);
}

public class StopPlayerMovementOnClickAction : StateAction
{
  InputReader _inputReader = default;
  private Player _player;

  public StopPlayerMovementOnClickAction(InputReader inputReader)
  {
    _inputReader = inputReader;
  }

  public override void Awake(StateMachine stateMachine)
  {
    _player = stateMachine.GetComponent<Player>();
  }

  public override void OnStateEnter()
  {
    _inputReader.MouseClickEvent += NotifyStopMoving;
    _inputReader.StopPlayerMovementOnClick = true;
  }

  public override void OnStateExit()
  {
    _inputReader.MouseClickEvent -= NotifyStopMoving;
    _inputReader.StopPlayerMovementOnClick = false;
  }

  public void NotifyStopMoving()
  {
    _player.OnNotifyStopMoving();
  }

  public override void OnUpdate() { }
}
