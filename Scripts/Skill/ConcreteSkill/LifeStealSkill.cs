using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LifeSteal", menuName = "Skill/LifeSteal")]
public class LifeStealSkill : SkillSO
{
  public float lifeStealScale = 0.2f;
  private GameObject _parent = default;
  private Attack _attackComp = default;
  private HealthSO _healthSO = default;
  private IntEventChanelSO _updateHealthUIEvent = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _followUpSkillActionEvent = default;

  public override void Setup(GameObject parent)
  {
    _parent = parent;
    _attackComp = parent.GetComponent<Attack>();
    _healthSO = parent.GetComponent<Damagable>()._currentHealth;
    _updateHealthUIEvent = parent.GetComponent<Damagable>().UpdateHealthUIEvent;
  }

  public override void Activate()
  {
    if (CurrentCooldownTime <= 0)
    {
      //active lifesteal effect trail
      _parent.GetComponent<SkillHolder>().ToggleLifeStealEffect();

      CurrentCooldownTime = CooldownTime;
      CurrentActiveTime = ActiveTime;

      UpdateSkillCooldownUIEvent.RaiseEvent(Index, CurrentCooldownTime);

      //TODO: kasih indicator active time diatas

      _followUpSkillActionEvent.OnEventRaised += ApplyLifeSteal;

      _attackComp.IsCastingSkill = true;
      _parent.GetComponent<Animator>().SetTrigger("BuffTrigger");
    }

  }

  public override void Deactivate()
  {
    _parent.GetComponent<SkillHolder>().ToggleLifeStealEffect();

    _followUpSkillActionEvent.OnEventRaised -= ApplyLifeSteal;
  }

  private void ApplyLifeSteal()
  {
    int lifeStealAmount = (int)(_attackComp._attackPoint * lifeStealScale);
    _healthSO.IncreaseHealth(lifeStealAmount);
    _updateHealthUIEvent.RaiseEvent(_healthSO.CurrentHealth);
  }

}
