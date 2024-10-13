using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillContainer", menuName = "Skill/SkillContainer")]
public class SkillContainerSO : ScriptableObject
{
  public List<SkillSO> LockedSkills = new List<SkillSO>();
  public List<SkillSO> UnlockedSkills = new List<SkillSO>();

  [Header("Listening on")]
  [SerializeField] private IntEventChanelSO _checkSkillTobeUnlockedEvent = default;

  private void Start()
  {
    UnlockedSkills.Clear();
    LockedSkills.Clear();

    LockedSkills.Add(CreateInstance<LifeStealSkill>());
  }

  private void OnEnable()
  {
    _checkSkillTobeUnlockedEvent.OnEventRaised += CheckLockedSkillToBeUnlocked;
  }

  private void OnDisable()
  {
    _checkSkillTobeUnlockedEvent.OnEventRaised -= CheckLockedSkillToBeUnlocked;
  }

  public void CheckLockedSkillToBeUnlocked(int currentLevel)
  {
    int length = LockedSkills.Count;
    for (int i = 0; i < length; i++)
    {
      if (currentLevel >= LockedSkills[i].UnlockLevel)
      {
        UnlockedSkills.Add(LockedSkills[i]);
        LockedSkills.Remove(LockedSkills[i]);
      }
    }
  }
}
