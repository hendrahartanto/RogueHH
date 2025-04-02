using UnityEngine;
using UnityEngine.Events;

public class UIMainMenu : MonoBehaviour
{
  public UIButtonBase ContinueButtonComp;
  public UnityAction NewGameButtonAction;
  public UnityAction ContinueButtonAction;
  public UnityAction ExitButtonAction;

  public void NewGameButtonOnClick()
  {
    NewGameButtonAction.Invoke();
  }

  public void ContinueButtonOnClick()
  {
    ContinueButtonAction.Invoke();
  }

  public void ExitButtonOnClick()
  {
    ExitButtonAction.Invoke();
  }
}
