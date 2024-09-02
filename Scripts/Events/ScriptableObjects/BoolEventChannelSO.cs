using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Channels/BoolEventChannel")]
public class BoolEventChannelSO : ScriptableObject
{
  public event UnityAction<bool> OnEventRaised;

  public void RaiseEvent(bool value)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(value);
  }
}
