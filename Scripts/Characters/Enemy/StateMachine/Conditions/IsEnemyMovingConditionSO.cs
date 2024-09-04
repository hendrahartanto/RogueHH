using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsEnemyMoving", menuName = "StateMachine/Conditions/Enemy/IsEnemyMoving")]
public class IsEnemyMovingConditionSO : StateConditionSO<IsEnemyMovingCondition> { }

public class IsEnemyMovingCondition : Condition
{
  private Enemy _enemy;

  public override void Awake(StateMachine stateMachine)
  {
    _enemy = stateMachine.GetComponent<Enemy>();
  }

  protected override bool Statement()
  {
    return _enemy.IsMoving;
  }
}
