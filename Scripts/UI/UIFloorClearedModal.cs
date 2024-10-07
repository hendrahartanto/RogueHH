using UnityEngine;
using UnityEngine.Events;

public class UIFloorClearedModal : MonoBehaviour
{
  public UnityAction ButtonAction;

  public void ContinueButtonOnClick()
  {
    ButtonAction.Invoke();
  }
}
