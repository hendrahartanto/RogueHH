using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovePlayerOnClickAction", menuName = "StateMachine/Actions/Pointer/MovePlayerOnClickAction")]
public class MovePlayerOnClickActionSO : StateActionSO
{
  [SerializeField] InputReader _inputReader = default;
  private Player _player;

  public override void InitComponent(StateMachine stateMachine)
  {
    _player = stateMachine.GetComponent<Player>();
    Debug.Log(_player);
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
