using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsCastingSkill", menuName = "StateMachine/Conditions/IsCastingSkill")]
public class IsCastingSkillConditionSO : StateConditionSO<IsCastingSkillCondition> { }

public class IsCastingSkillCondition : Condition
{
  private Attack _attack;

  public override void Awake(StateMachine stateMachine)
  {
    _attack = stateMachine.GetComponent<Attack>();
  }

  protected override bool Statement()
  {
    return _attack.IsCastingSkill;
  }
}
