using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
  public GameObject SkillIconObject = default;
  public Image SKillIconImage = default;
  public GameObject CooldownIndicatorObject = default;
  public TextMeshProUGUI CooldownText = default;

  private void Awake()
  {
    SKillIconImage = SkillIconObject.GetComponent<Image>();
    CooldownText = CooldownIndicatorObject.GetComponentInChildren<TextMeshProUGUI>();
  }
}
