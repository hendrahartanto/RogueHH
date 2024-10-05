using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonBase : MonoBehaviour
{
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
}
