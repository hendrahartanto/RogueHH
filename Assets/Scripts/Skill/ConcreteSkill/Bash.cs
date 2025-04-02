using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bash", menuName = "Skill/Bash")]
public class Bash : SkillSO
{
  public float DamageSclaing = default;
  private GameObject _parent = default;
  private Attack _attackComp = default;
  private SkillHolder _skillHolderComp = default;

  public override void Setup(GameObject parent)
  {
    _parent = parent;
    _attackComp = parent.GetComponent<Attack>();
    _skillHolderComp = parent.GetComponent<SkillHolder>();
  }

  public override void SetupDescription()
  {
    Description = Name + " - Unleash powerful strike to the head, dealing " + DamageSclaing * 100 + "% of your attack as physical damage.";
  }

  public override void Activate()
  {
    if (CurrentCooldownTime <= 0)
    {
      //if this skill is currently selected deactive this skill
      if (_skillHolderComp.SelectedSkill == this)
      {
        Deactivate();
        return;
      }

      //set this skill to be selected
      _skillHolderComp.SelectSkill(this);
      _attackComp.IsSkillActive = true;
    }
  }

  public override void Deactivate()
  {
    //deselect this skill
    _skillHolderComp.SelectSkill(this);
    _attackComp.IsSkillActive = false;
  }

  public override void ExecuteSkillAction()
  {
    _attackComp.IsCastingSkill = true;
    _parent.GetComponent<Animator>().SetTrigger("BashTrigger");

    CurrentCooldownTime = CooldownTime + 1;
    _attackComp.SkillMultiplier = DamageSclaing;
  }
}
