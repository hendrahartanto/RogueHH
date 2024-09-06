using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarManager : MonoBehaviour
{
  private Slider _slider;

  [Header("Listening to")]
  public IntEventChanelSO SetMaxHealthUIEvent = default;
  public IntEventChanelSO UpdateHealthUIEvent = default;

  private void Awake()
  {
    _slider = GetComponent<Slider>();
  }

  private void OnEnable()
  {
    SetMaxHealthUIEvent.OnEventRaised += SetMaxHealthUI;
    UpdateHealthUIEvent.OnEventRaised += UpdateHealthUI;
  }

  private void OnDisable()
  {
    SetMaxHealthUIEvent.OnEventRaised -= SetMaxHealthUI;
    UpdateHealthUIEvent.OnEventRaised -= UpdateHealthUI;
  }

  private void SetMaxHealthUI(int healthValue)
  {
    _slider.maxValue = healthValue;
    _slider.value = healthValue;
  }

  private void UpdateHealthUI(int healthValue)
  {
    _slider.value = healthValue;
  }
}
