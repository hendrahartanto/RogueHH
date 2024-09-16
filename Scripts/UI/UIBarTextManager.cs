using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBarTextManager : MonoBehaviour
{
  private int _maxHealth = default;
  private TextMeshProUGUI _textMesh = default;

  [Header("Listening to")]
  [SerializeField] private IntEventChanelSO _setMaxHealthUIEvent = default;
  [SerializeField] private IntEventChanelSO _updateHealthUIEvent = default;

  private void Awake()
  {
    _textMesh = GetComponentInChildren<TextMeshProUGUI>();
  }

  private void OnEnable()
  {
    _setMaxHealthUIEvent.OnEventRaised += SetMaxHealth;
    _updateHealthUIEvent.OnEventRaised += SetCurrentHealthText;
  }

  private void OnDisable()
  {
    _setMaxHealthUIEvent.OnEventRaised -= SetMaxHealth;
    _updateHealthUIEvent.OnEventRaised -= SetCurrentHealthText;
  }

  private void SetMaxHealth(int maxHealthValue)
  {
    _maxHealth = maxHealthValue;
    _textMesh.SetText(maxHealthValue.ToString() + "/" + _maxHealth.ToString());
  }

  private void SetCurrentHealthText(int healthValue)
  {
    _textMesh.SetText(healthValue.ToString() + "/" + _maxHealth.ToString());
  }


}
