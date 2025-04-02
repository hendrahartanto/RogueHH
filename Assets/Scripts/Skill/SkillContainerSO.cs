using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillContainer", menuName = "Skill/SkillContainer")]
public class SkillContainerSO : ScriptableObject
{
  public List<SkillSO> PersistentLockedSkills = new List<SkillSO>();
  public List<SkillSO> LockedSkills = new List<SkillSO>();
  public List<SkillSO> UnlockedSkills = new List<SkillSO>();

  [Header("Listening on")]
  [SerializeField] private IntEventChanelSO _checkSkillTobeUnlockedEvent = default;

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
    for (int i = LockedSkills.Count - 1; i >= 0; i--)
    {
      if (currentLevel >= LockedSkills[i].UnlockLevel)
      {
        UnlockedSkills.Insert(0, LockedSkills[i]);
        LockedSkills.RemoveAt(i);
      }
    }
  }

  public void Reset()
  {
    UnlockedSkills.Clear();
    LockedSkills.Clear();

    foreach (SkillSO skill in PersistentLockedSkills)
    {
      LockedSkills.Add(Instantiate(skill));
    }
  }
}
