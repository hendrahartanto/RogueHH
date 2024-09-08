using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/Vector3_Int_EventChanel")]
public class Vector3_Int_EvetChanel : ScriptableObject
{
  public event UnityAction<Vector3, int> OnEventRaised;
  public void RaiseEvent(Vector3 pos, int value)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(pos, value);
  }
}
