using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyReadyToAttack", menuName = "StateMachine/Actions/Enemy/EnemyReadyToAttack")]
public class EnemyReadyToAttackActionSO : StateActionSO<EnemyReadyToAttackAction> { }

public class EnemyReadyToAttackAction : StateAction
{
  private AttackRangeTrigger _attackRangeTrigger;
  private Enemy _enemy;
  private Attack _attack;

  public override void Awake(StateMachine stateMachine)
  {
    _attackRangeTrigger = stateMachine.GetComponent<AttackRangeTrigger>();
    _enemy = stateMachine.GetComponent<Enemy>();
    _attack = stateMachine.GetComponent<Attack>();
  }

  public override void OnStateEnter()
  {
    Debug.Log("ASSIGN READY TO ATTACK");
    _enemy.OnTurnExecuted += AttackTarget;
  }

  public override void OnStateExit()
  {
    Debug.Log("REMOVE READY TO ATTACK");
    _enemy.OnTurnExecuted -= AttackTarget;
  }

  private void AttackTarget()
  {
    if (_attackRangeTrigger.TargetList[0].TryGetComponent(out Damagable damagableComp))
    {
      _attack.AttacTarget(damagableComp);
    }
  }

  public override void OnUpdate() { }
}
