using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
  [SerializeField] private InputReader _inputReader = default;
  public List<GameObject> LifeStealEffect = new List<GameObject>();
  public SkillContainerSO SkillVault = default;
  public List<SkillSO> Skills = new List<SkillSO>();
  public SkillSO SelectedSkill = default;

  private void Awake()
  {

  }

  private void Start()
  {
    int index = 0;
    foreach (SkillSO skillTempelate in SkillVault.UnlockedSkills)
    {
      SkillSO skillInstance = Instantiate(skillTempelate);

      skillInstance.Setup(gameObject);

      SetupSkillAction(index, skillInstance);

      Skills.Add(skillInstance);

      index++;
    }
  }

  private void SetupSkillAction(int index, SkillSO skill)
  {
    if (index == 0)
    {
      _inputReader.Skill1Action += skill.Activate;
    }
  }

  //called by animatior event
  public void ToggleLifeStealEffect()
  {
    foreach (GameObject effect in LifeStealEffect)
    {
      effect.SetActive(!effect.activeSelf);
    }
  }
}
