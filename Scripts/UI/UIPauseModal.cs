using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPauseModal : MonoBehaviour
{
  public UnityAction ResumeButtonAction;
  public UnityAction BackToUpgradeButtonAction;
  public UnityAction ExitToMainMenuButtonAction;

  public void ResumeButtonOnClick()
  {
    ResumeButtonAction.Invoke();
  }

  public void BackToUpgradeButtonOnClick()
  {
    BackToUpgradeButtonAction.Invoke();
  }

  public void ExitTOMainMenuButtonOnClick()
  {
    ExitToMainMenuButtonAction.Invoke();
  }

}

