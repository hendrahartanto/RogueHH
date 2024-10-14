using System.Collections.Generic;
using UnityEngine;

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

    foreach (SkillSO lockedSkill in SkillVault.LockedSkills)
    {
      InsertLockedSkill(lockedSkill, index);

      index++;
    }

    foreach (SkillSO unlockedSkill in SkillVault.UnlockedSkills)
    {
      InsertSkill(unlockedSkill, index);

      UpdateSKillCooldownUI(index, unlockedSkill.CurrentActiveTime);

      index++;
    }
  }

  private void InsertLockedSkill(SkillSO skill, int index)
  {
    UISkill skillComp = UISkillList[index];
    skillComp.SetOccupied();

    skillComp.LockedSkillOverlayObject.SetActive(true);

    SetupSkillIcon(skillComp, skill.SkillIcon);
    SetupSkillDescription(skillComp, skill, 1);
  }
  private void InsertSkill(SkillSO skill, int index)
  {
    UISkill skillComp = UISkillList[index];
    skillComp.SetOccupied();

    skillComp.LockedSkillOverlayObject.SetActive(false);

    SetupSkillIcon(skillComp, skill.SkillIcon);
    SetupSkillDescription(skillComp, skill, 0);
  }

  private void SetupSkillIcon(UISkill uISkill, Sprite icon)
  {
    uISkill.SKillIconImage.enabled = true;
    uISkill.SKillIconImage.sprite = icon;
  }

  private void SetupSkillDescription(UISkill uiSKill, SkillSO skill, int type)
  {
    if (type == 0)
      skill.SetupDescription();
    else
      skill.Description = "Unlocked at level " + (skill.UnlockLevel + 1);

    uiSKill.DescriptionText.SetText(skill.Description);
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
