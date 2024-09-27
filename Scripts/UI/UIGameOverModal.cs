using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGameOverModal : MonoBehaviour
{
  [SerializeField] private Button _continueButton = default;

  public UnityAction ContinueButtonAction;

  public void ContinueButtonOnClick()
  {
    ContinueButtonAction.Invoke();
  }
}
