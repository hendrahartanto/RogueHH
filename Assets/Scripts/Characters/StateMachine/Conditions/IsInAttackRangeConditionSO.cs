using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsInAttackRange", menuName = "StateMachine/Conditions/IsInAttackRange")]
public class IsInAttackRangeConditionSO : StateConditionSO<IsInAttackRangeCondition> { }

public class IsInAttackRangeCondition : Condition
{
  private AttackRangeTrigger _attackRangeTrigger;

  public override void Awake(StateMachine stateMachine)
  {
    _attackRangeTrigger = stateMachine.GetComponent<AttackRangeTrigger>();
  }

  protected override bool Statement()
  {
    return _attackRangeTrigger.IsInAttackRange;
  }
}
