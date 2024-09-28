using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health", menuName = "Gameplay/Character/Health")]
public class HealthSO : ScriptableObject
{
  private int _maxHealth;
  private int _currentHealth;

  public int MaxHealth => _maxHealth;
  public int CurrentHealth => _currentHealth;

  public void SetMaxHealth(int value)
  {
    _maxHealth = value;
  }

  public void SetCurrentHealth(int value)
  {
    _currentHealth = value;
  }

  public void DecreaseHealth(int damageValue)
  {
    _currentHealth -= damageValue;
    if (_currentHealth < 0)
      _currentHealth = 0;
  }
}
