using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
  public UnityAction OnEventRaised;

  public void RaiseEvent()
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke();
  }
}
