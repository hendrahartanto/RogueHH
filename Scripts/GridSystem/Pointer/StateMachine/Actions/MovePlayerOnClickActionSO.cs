using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovePlayerOnClickAction", menuName = "StateMachine/Actions/Pointer/MovePlayerOnClickAction")]
public class MovePlayerOnClickActionSO : StateActionSO
{
  public InputReader InputReader = default;
  protected override StateAction CreateAction() => new MovePlayerOnClickAction(InputReader);
}

public class MovePlayerOnClickAction : StateAction
{
  InputReader _inputReader = default;
  private Player _player;

  public MovePlayerOnClickAction(InputReader inputReader)
  {
    _inputReader = inputReader;
  }

  public override void Awake(StateMachine stateMachine)
  {
    _player = stateMachine.GetComponent<Player>();
  }

  public override void OnStateEnter()
  {
    _inputReader.MouseClickEvent += NotifyMovePlayer;
  }

  public override void OnStateExit()
  {
    _inputReader.MouseClickEvent -= NotifyMovePlayer;
  }

  public void NotifyMovePlayer()
  {
    _player.OnNotifyMovePlayer();
  }

  public override void OnUpdate() { }
}
