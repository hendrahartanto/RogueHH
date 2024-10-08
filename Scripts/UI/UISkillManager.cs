using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UISkillManager : MonoBehaviour
{
  public SkillContainerSO SkillVault = default;
  public List<UISkill> UISkillList = new List<UISkill>();

  [Header("Listening on")]
  [SerializeField] private IntIntEventChannelSO _updateSkillCooldownUIEvent = default;
  [SerializeField] private VoidEventChannelSO _onLocationReady = default;

  private void OnEnable()
  {
    _updateSkillCooldownUIEvent.OnEventRaised += UpdateSKillCooldownUI;
    _onLocationReady.OnEventRaised += SetupSkillUI;
  }

  private void OnDisable()
  {
    _updateSkillCooldownUIEvent.OnEventRaised -= UpdateSKillCooldownUI;
    _onLocationReady.OnEventRaised -= SetupSkillUI;
  }

  private void SetupSkillUI()
  {
    int index = 0;
    foreach (SkillSO skillTempelate in SkillVault.UnlockedSkills)
    {
      SkillSO skillInstance = Instantiate(skillTempelate);

      InsertSkill(skillInstance, index);

      UpdateSKillCooldownUI(index, skillInstance.CurrentActiveTime);

      index++;
    }
  }

  private void InsertSkill(SkillSO skill, int index)
  {
    SetupSkillIcon(UISkillList[index], skill.SkillIcon);
  }

  private void SetupSkillIcon(UISkill uISkill, Sprite icon)
  {
    uISkill.SKillIconImage.enabled = true;
    uISkill.SKillIconImage.sprite = icon;
  }

  private void UpdateSKillCooldownUI(int index, int currentCooldownTime)
  {
    if (currentCooldownTime > 0)
    {
      UISkillList[index].CooldownIndicatorObject.SetActive(true);
      UISkillList[index].CooldownText.SetText(currentCooldownTime.ToString());
    }
    else
    {
      UISkillList[index].CooldownIndicatorObject.SetActive(false);
    }
  }
}
