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
  public AudioCueSO LifeStealBuffSound = default;

  [Header("Broadcasting to")]
  private IntEventChanelSO _updateHealthUIEvent = default;
  [SerializeField] private TextPopupEventChannelSO _textPopupEvent = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _followUpSkillActionEvent = default;

  public override void Setup(GameObject parent)
  {
    _parent = parent;
    _attackComp = parent.GetComponent<Attack>();
    _healthSO = parent.GetComponent<Damagable>()._currentHealth;
    _updateHealthUIEvent = parent.GetComponent<Damagable>().UpdateHealthUIEvent;
  }

  private void OnDestroy()
  {
    _followUpSkillActionEvent.OnEventRaised -= ApplyLifeSteal;
  }

  public override void SetupDescription()
  {
    Description = Name + " - For each successfull hit, increase your health by " + lifeStealScale * 100 + "% of your attack";
  }

  public override void Activate()
  {
    if (CurrentCooldownTime <= 0)
    {
      _parent.GetComponent<SkillHolder>().ToggleLifeStealEffect();

      CurrentCooldownTime = CooldownTime;
      CurrentActiveTime = ActiveTime;

      UpdateSkillCooldownUIEvent.RaiseEvent(Index, CurrentCooldownTime);

      UpdateUIIndicator.RaiseEvent(this, Index);

      _attackComp.IsCastingSkill = true;
      _parent.GetComponent<Animator>().SetTrigger("BuffTrigger");

      _parent.GetComponent<HumanAudio>().Buff = LifeStealBuffSound;

      //action
      _followUpSkillActionEvent.OnEventRaised += ApplyLifeSteal;
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

    _textPopupEvent.RaiseEvent(_parent.transform.position, lifeStealAmount.ToString(), TextColor.Green, -0.2f);
  }

}
