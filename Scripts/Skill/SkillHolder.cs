using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
  [SerializeField] private InputReader _inputReader = default;
  public List<GameObject> LifeStealEffect = new List<GameObject>();
  public GameObject BuffAuraEffect;
  public SkillContainerSO SkillVault = default;
  public List<SkillSO> Skills = new List<SkillSO>();
  public SkillSO SelectedSkill = default;

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

  //untuk destory instansi SO nya
  private void OnDestroy()
  {
    foreach (SkillSO skill in Skills)
    {
      if (skill != null)
      {
        Destroy(skill);
      }
    }
  }

  private void SetupSkillAction(int index, SkillSO skill)
  {
    if (index == 0)
      _inputReader.Skill1Action += skill.Activate;
    else if (index == 1)
      _inputReader.Skill2Action += skill.Activate;
  }

  public void ToggleLifeStealEffect()
  {
    foreach (GameObject effect in LifeStealEffect)
    {
      effect.SetActive(!effect.activeSelf);
    }
  }

  public void ToggleBuffAuraEffect()
  {
    BuffAuraEffect.SetActive(!BuffAuraEffect.activeSelf);
  }
}
