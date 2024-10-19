using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIAlertModal : MonoBehaviour
{
  public UnityAction ContinueButtonAction;
  public UnityAction BackButtonAction;

  [Header("Broadcasting to")]
  [SerializeField] private PlaySFXEventChannelSO _playSFXEvent = default;

  private void OnEnable()
  {
    _playSFXEvent.RaiseEvent(SFXName.MenuOpen, transform);
  }

  private void OnDisable()
  {
    _playSFXEvent.RaiseEvent(SFXName.MenuClosed, transform);
  }

  public void ContinueButtonOnClick()
  {
    ContinueButtonAction.Invoke();
  }

  public void BackButtonOnClick()
  {
    BackButtonAction.Invoke();
  }
}
