using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StopPlayerMovementOnClickAction", menuName = "StateMachine/Actions/Pointer/StopPlayerMovementOnClickAction")]
public class StopPlayerMovementOnClickActionSO : StateActionSO<StopPlayerMovementOnClickAction>
{
  public InputReader InputReader = default;
  protected override StateAction CreateAction() => new StopPlayerMovementOnClickAction();
}

public class StopPlayerMovementOnClickAction : StateAction
{
  InputReader _inputReader = default;
  private Player _player;

  public override void Awake(StateMachine stateMachine)
  {
    _inputReader = ((StopPlayerMovementOnClickActionSO)OriginSO).InputReader;
    _player = stateMachine.GetComponent<Player>();
  }

  public override void OnStateEnter()
  {
    _inputReader.MouseClickEvent += NotifyStopMoving;
  }

  public override void OnStateExit()
  {
    _inputReader.MouseClickEvent -= NotifyStopMoving;
  }

  public void NotifyStopMoving()
  {
    _player.OnNotifyStopMoving();
  }

  public override void OnUpdate() { }
}
