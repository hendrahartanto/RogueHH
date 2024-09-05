using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsAttacking", menuName = "StateMachine/Conditions/IsAttacking")]
public class IsAttackingConditionSO : StateConditionSO<IsAttackingCondition> { }

public class IsAttackingCondition : Condition
{
  private Attack _attack;

  public override void Awake(StateMachine stateMachine)
  {
    _attack = stateMachine.GetComponent<Attack>();
  }

  protected override bool Statement()
  {
    return _attack.IsAttacking;
  }
}
