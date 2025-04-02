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

  [Header("Broadcasting to")]
  [SerializeField] private IntEventChanelSO _toggleSelectedUIEvent = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _executeActiveSkillEvent = default;

  private void OnEnable()
  {
    _executeActiveSkillEvent.OnEventRaised += ExecuteCurrentActiveSkillAction;
  }

  private void OnDisable()
  {
    _executeActiveSkillEvent.OnEventRaised -= ExecuteCurrentActiveSkillAction;
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
    else if (index == 2)
      _inputReader.Skill3Action += skill.Activate;
  }

  public void SelectSkill(SkillSO skillSO)
  {
    if (SelectedSkill != null)
      ToggleSkilUI(SelectedSkill);

    if (SelectedSkill == skillSO)
      SelectedSkill = null;
    else
    {
      SelectedSkill = skillSO;
      ToggleSkilUI(skillSO);
    }
  }

  public void ToggleSkilUI(SkillSO skillSO)
  {
    _toggleSelectedUIEvent.RaiseEvent(skillSO.Index);
  }

  private void ExecuteCurrentActiveSkillAction()
  {
    SelectedSkill.ExecuteSkillAction();

    ToggleSkilUI(SelectedSkill);
    SelectedSkill = null;
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
