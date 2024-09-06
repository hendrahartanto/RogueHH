using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsDead", menuName = "StateMachine/Conditions/IsDead")]
public class IsDeadConditionSO : StateConditionSO<IsDeadCondition> { }

public class IsDeadCondition : Condition
{
  private Damagable _damagable;

  public override void Awake(StateMachine stateMachine)
  {
    _damagable = stateMachine.GetComponent<Damagable>();
  }

  protected override bool Statement()
  {
    return _damagable.IsDead;
  }
}