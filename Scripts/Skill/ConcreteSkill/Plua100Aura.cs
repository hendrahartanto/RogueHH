using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plus100Aura", menuName = "Skill/Plus100Aura")]
public class Plua100Aura : SkillSO
{
  public float BuffScale = 0.2f;
  private int _attackBonus = default;
  private int _deffendBonus = default;
  private GameObject _parent = default;
  private Attack _attackComp = default;
  private Damagable _damaagbleComp = default;

  public override void Setup(GameObject parent)
  {
    _parent = parent;
    _attackComp = parent.GetComponent<Attack>();
    _damaagbleComp = parent.GetComponent<Damagable>();

    _attackBonus = (int)(_attackComp._attackPoint * BuffScale);
    _deffendBonus = (int)(_damaagbleComp.DeffendPoint * BuffScale);
  }

  public override void SetupDescription()
  {
    Description = Name + " - Increase your aura by 100, Improve your attack and deffend " + BuffScale * 100 + "%";
  }

  public override void Activate()
  {
    if (CurrentCooldownTime <= 0)
    {
      CurrentCooldownTime = CooldownTime;
      CurrentActiveTime = ActiveTime;

      UpdateSkillCooldownUIEvent.RaiseEvent(Index, CurrentCooldownTime);

      UpdateUIIndicator.RaiseEvent(this, Index);

      _attackComp.IsCastingSkill = true;

      _parent.GetComponent<Animator>().SetTrigger("BuffTrigger");

      //action
      _attackComp._attackPoint += _attackBonus;
      _damaagbleComp.DeffendPoint += _deffendBonus;
    }
  }

  public override void Deactivate()
  {
    _attackComp._attackPoint -= _attackBonus;
    _damaagbleComp.DeffendPoint -= _deffendBonus;
  }

}
