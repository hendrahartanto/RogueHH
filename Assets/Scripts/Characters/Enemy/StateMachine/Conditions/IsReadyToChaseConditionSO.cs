using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsReadyToChase", menuName = "StateMachine/Conditions/Enemy/IsReadyToChase")]
public class IsReadyToChaseConditionSO : StateConditionSO<IsReadyToChaseCondition> { }

public class IsReadyToChaseCondition : Condition
{
  private EnemyAggroTrigger _enemyAggroTrigger;

  public override void Awake(StateMachine stateMachine)
  {
    _enemyAggroTrigger = stateMachine.GetComponent<EnemyAggroTrigger>();
  }
  protected override bool Statement()
  {
    return _enemyAggroTrigger.IsReadyToChase;
  }
}
