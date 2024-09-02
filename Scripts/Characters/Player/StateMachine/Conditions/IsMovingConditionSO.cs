using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsMoving", menuName = "StateMachine/Conditions/Player/IsMoving")]
public class IsMovingConditionSO : StateConditionSO<IsMovingCondition> { }

public class IsMovingCondition : Condition
{
  private Player _player;

  public override void Awake(StateMachine stateMachine)
  {
    _player = stateMachine.GetComponent<Player>();
  }

  protected override bool Statement()
  {
    return _player.IsMoving;
  }
}
