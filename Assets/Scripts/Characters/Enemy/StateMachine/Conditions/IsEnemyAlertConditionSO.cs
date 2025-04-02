using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsEnemyAlert", menuName = "StateMachine/Conditions/Enemy/IsEnemyAlert")]
public class IsEnemyAlertConditionSO : StateConditionSO<IsEnemyAlertCondition> { }

public class IsEnemyAlertCondition : Condition
{
  private EnemyAggroTrigger _enemyAggroTriggerComp = default;

  public override void Awake(StateMachine stateMachine)
  {
    _enemyAggroTriggerComp = stateMachine.GetComponent<EnemyAggroTrigger>();
  }
  protected override bool Statement()
  {
    return _enemyAggroTriggerComp.IsEnemyAlert;
  }
}
