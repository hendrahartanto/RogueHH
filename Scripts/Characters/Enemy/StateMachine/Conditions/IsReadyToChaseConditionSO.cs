using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsReadyToChase", menuName = "StateMachine/Conditions/Enemy/IsReadyToChase")]
public class IsReadyToChaseConditionSO : StateConditionSO
{
  private Enemy _enemy;

  public override void InitComponent(StateMachine stateMachine)
  {
    _enemy = stateMachine.GetComponent<Enemy>();
  }
  protected override bool Statement()
  {
    return _enemy.IsReadyToChase;
  }
}
