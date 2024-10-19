using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonBase : MonoBehaviour
{
  public Button ButtonComp;
  public GameObject DisabledOverlayObject;
  public EventTrigger EventTriggerComp;

  [Header("Broadcasting to")]
  [SerializeField] private PlaySFXEventChannelSO _playSFXEvent = default;

  public void PlaySFXMenuSelect()
  {
    _playSFXEvent.RaiseEvent(SFXName.MenuSelect, transform);
  }

  public void PlaySFXMenuClick()
  {
    _playSFXEvent.RaiseEvent(SFXName.MenuClick, transform);
  }

  public void DisableButton()
  {
    ButtonComp.interactable = false;
    DisabledOverlayObject.SetActive(false);
    EventTriggerComp.enabled = false;
  }
}
