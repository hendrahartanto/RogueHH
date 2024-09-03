using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovePlayerOnClickAction", menuName = "StateMachine/Actions/Pointer/MovePlayerOnClickAction")]
public class MovePlayerOnClickActionSO : StateActionSO
{
  public InputReader InputReader = default;
  public GameStateSO GameState = default;
  protected override StateAction CreateAction() => new MovePlayerOnClickAction(InputReader, GameState);
}

public class MovePlayerOnClickAction : StateAction
{
  InputReader _inputReader = default;
  private Player _player;
  private GameStateSO _gameState;

  public MovePlayerOnClickAction(InputReader inputReader, GameStateSO gameState)
  {
    _inputReader = inputReader;
    _gameState = gameState;
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
    bool isInCombat = _gameState.CurrentGameState == GameState.Combat;
    _player.OnNotifyMovePlayer(isInCombat);
  }

  public override void OnUpdate() { }
}
