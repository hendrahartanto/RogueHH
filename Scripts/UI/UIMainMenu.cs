using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
  public UIButtonBase ContinueButtonComp;
  public UnityAction NewGameButtonAction;
  public UnityAction ContinueButtonAction;

  public void NewGameButtonOnClick()
  {
    NewGameButtonAction.Invoke();
  }

  public void ContinueButtonOnClick()
  {
    ContinueButtonAction.Invoke();
  }
}
