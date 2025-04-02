using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCheckAggroAction", menuName = "StateMachine/Actions/Enemy/EnemyCheckAggroAction")]
public class EnemyCheckAggroActionSO : StateActionSO<EnemyCheckAggroAction> { }

public class EnemyCheckAggroAction : StateAction
{
  private EnemyAggroTrigger _enemyAggroTriggerComp = default;
  private Enemy _enemy = default;

  public override void Awake(StateMachine stateMachine)
  {
    _enemyAggroTriggerComp = stateMachine.GetComponent<EnemyAggroTrigger>();
    _enemy = stateMachine.GetComponent<Enemy>();
  }

  public override void OnStateEnter()
  {
    _enemy.OnTurnExecuted += _enemyAggroTriggerComp.CheckAggro;
  }

  public override void OnStateExit()
  {
    _enemy.OnTurnExecuted -= _enemyAggroTriggerComp.CheckAggro;
  }

  public override void OnUpdate() { }
}
