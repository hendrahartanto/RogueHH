using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsGettingHit", menuName = "StateMachine/Conditions/IsGettingHit")]
public class IsGettingHitConditionSO : StateConditionSO<IsGettingHitCondition> { }

public class IsGettingHitCondition : Condition
{
  private Damagable _damagable;

  public override void Awake(StateMachine stateMachine)
  {
    _damagable = stateMachine.GetComponent<Damagable>();
  }

  protected override bool Statement()
  {
    return _damagable.IsGettingHit && !_damagable.IsDead;
  }
}
