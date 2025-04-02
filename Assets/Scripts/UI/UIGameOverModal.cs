using UnityEngine;
using UnityEngine.Events;

public class UIGameOverModal : MonoBehaviour
{
  public UnityAction ContinueButtonAction;

  public void ContinueButtonOnClick()
  {
    ContinueButtonAction.Invoke();
  }
}
