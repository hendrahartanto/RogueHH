using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyMoveAction", menuName = "StateMachine/Actions/Enemy/EnemyMoveAction")]
public class EnemyMoveActionSO : StateActionSO<EnemyMoveAction> { }

public class EnemyMoveAction : StateAction
{
  private Enemy _enemy = default;
  public override void Awake(StateMachine stateMachine)
  {
    _enemy = stateMachine.GetComponent<Enemy>();
  }

  public override void OnStateEnter()
  {
    _enemy.OnTurnExecuted += _enemy.OnNotifyMoveEnemy;
  }

  public override void OnStateExit()
  {
    _enemy.OnTurnExecuted -= _enemy.OnNotifyMoveEnemy;
  }

  public override void OnUpdate() { }
}