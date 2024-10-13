using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/UpdateUIIndicatorChannel")]
public class UpdateUIIndicatorChannelSO : ScriptableObject
{
  public event UnityAction<SkillSO, int> OnEventRaised;

  public void RaiseEvent(SkillSO skillSO, int index)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(skillSO, index);
  }
}
