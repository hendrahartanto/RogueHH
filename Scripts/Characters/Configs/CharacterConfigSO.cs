using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "CharacterConfigSO", menuName = "Configs/Character/CharacterConfigSO")]
[System.Serializable]
public class CharacterConfigSO : ScriptableObject
{
  [Header("Character config")]
  public int MinInitialHealth;
  public int MaxInitialHealth;
  public int MinInitialAttackPoint;
  public int MaxInitialAttackPoint;
  public int MinInitialDeffendPoint;
  public int MaxInitialDeffendPoint;
  public float CriticalRate;
  public float CriticalDamage;
  public int WeaponType;
  public int Level;

  [Header("Player config")]
  public int ExpCap;
  public AppearanceType appearanceType;

  [Header("Enemy config")]
  public int MinExpGain;
  public int MaxExpGain;
  public int MinGoldGain;
  public int MaxGoldGain;
  public float ExpGainScalingFactor;
  public float GoldGainScalingFactor;

  private void Awake()
  {
    ExpGainScalingFactor = 1.1f;
    GoldGainScalingFactor = 1.1f;
  }

  public void Reset()
  {
    MinInitialHealth = 20;
    MaxInitialHealth = 20;
    MinInitialAttackPoint = 5;
    MaxInitialAttackPoint = 5;
    MinInitialDeffendPoint = 5;
    MaxInitialDeffendPoint = 5;
    CriticalRate = 0.05f;
    CriticalDamage = 1.5f;
    Level = 0;
  }

  public int GetInitialHealth()
  {
    int baseHealth = Random.Range(MinInitialHealth, MaxInitialHealth + 1);

    return (int)(baseHealth * Math.Pow(1.1, Level));
  }

  public int GetInitialAttackPoint()
  {
    int baseAttack = Random.Range(MinInitialAttackPoint, MaxInitialAttackPoint + 1);

    return (int)(baseAttack * (1 + 0.12 * Level));
  }

  public int GetInitialDeffendPoint()
  {
    int baseDeffenPoint = Random.Range(MinInitialDeffendPoint, MaxInitialDeffendPoint + 1);

    return (int)(baseDeffenPoint + (Level * 0.08 * baseDeffenPoint));
  }

  public int GetExpGain()
  {
    int baseExp = Random.Range(MinExpGain, MaxExpGain + 1);
    float scaledExp = baseExp * Mathf.Pow(ExpGainScalingFactor, Level);
    return Mathf.FloorToInt(scaledExp);
  }

  public int GetGoldGain()
  {
    int baseGold = Random.Range(MinGoldGain, MaxGoldGain + 1);
    float scaledGold = baseGold * Mathf.Pow(GoldGainScalingFactor, Level);
    return Mathf.FloorToInt(scaledGold);
  }
}

public enum AppearanceType
{
  Naked,
  Normal,
  Elite
}