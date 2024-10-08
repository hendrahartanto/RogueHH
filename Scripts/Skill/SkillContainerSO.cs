using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillContainer", menuName = "Skill/SkillContainer")]
public class SkillContainerSO : ScriptableObject
{
  public List<SkillSO> LockedSkills = new List<SkillSO>();
  public List<SkillSO> UnlockedSkills = new List<SkillSO>();

  public void CheckLockedSkillToBeUnlocked(int currentPlayerLevel)
  {
    foreach (SkillSO skill in LockedSkills)
    {
      if (currentPlayerLevel >= skill.UnlockLevel)
      {
        UnlockedSkills.Add(skill);
        LockedSkills.Remove(skill);
      }
    }
  }
}
