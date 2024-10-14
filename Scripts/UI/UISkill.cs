using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
  public GameObject SkillIconObject = default;
  public Image SKillIconImage = default;
  public GameObject CooldownIndicatorObject = default;
  public TextMeshProUGUI CooldownText = default;
  public GameObject DescriptionObject = default;
  public TextMeshProUGUI DescriptionText = default;
  public GameObject LockedSkillOverlayObject = default;
  public Image LockedSkillOverlayImage = default;
  private bool _isSlotOccupied = false;

  private void Awake()
  {
    SKillIconImage = SkillIconObject.GetComponent<Image>();
    LockedSkillOverlayImage = DescriptionObject.GetComponent<Image>();

    CooldownText = CooldownIndicatorObject.GetComponentInChildren<TextMeshProUGUI>();
    DescriptionText = DescriptionObject.GetComponentInChildren<TextMeshProUGUI>();
  }

  public void DescriptionSetActive(bool isActive)
  {
    if (_isSlotOccupied)
      DescriptionObject.SetActive(isActive);
  }

  public void SetOccupied() => _isSlotOccupied = true;
}
