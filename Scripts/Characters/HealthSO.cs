using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSO : ScriptableObject
{
  private int _maxHelath;
  private int _currentHealth;

  public void SetMaxHealth(int value)
  {
    _maxHelath = value;
  }

  public void SetCurrentHealth(int value)
  {
    _currentHealth = value;
  }

  public void DecreaseHealth(int damageValue)
  {
    _currentHealth -= damageValue;
  }
}
