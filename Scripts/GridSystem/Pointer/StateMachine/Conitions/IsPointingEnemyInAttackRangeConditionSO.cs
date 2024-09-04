using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsPointingEnemyInAttackRange", menuName = "StateMachine/Conditions/Pointer/IsPointingEnemyInAttackRange")]
public class IsPointingEnemyInAttackRangeConditionSO : StateConditionSO<IsPointingEnemyInAttackRangeCondition> { }

public class IsPointingEnemyInAttackRangeCondition : Condition
{
  private AttackRangeTrigger _attackRangeTrigger;
  private PointerManager _pointerManager;

  public override void Awake(StateMachine stateMachine)
  {
    _attackRangeTrigger = stateMachine.GetComponent<AttackRangeTrigger>();
    _pointerManager = stateMachine.GetComponent<PointerManager>();
  }

  protected override bool Statement()
  {
    return _attackRangeTrigger.TargetList.Contains(_pointerManager.CurrentPointedCollider);
  }
}
