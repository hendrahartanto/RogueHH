using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/ChangeAppearanceEventChannel")]
public class ChangeAppearanceEventChannelSO : ScriptableObject
{
  public event UnityAction<AppearanceType> OnEventRaised;

  public void RaiseEvent(AppearanceType type)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(type);
  }
}

