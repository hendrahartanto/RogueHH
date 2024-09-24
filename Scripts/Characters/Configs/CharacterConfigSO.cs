using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfigSO", menuName = "Configs/Character/CharacterConfigSO")]
public class CharacterConfigSO : ScriptableObject
{
  [Header("Character config")]
  public int MinInitialHealth;
  public int MaxInitialHealth;
  public int MinInitialAttackPoint;
  public int MaxInitialAttackPoint;
  public int MinInitialDeffendPoint;
  public int MaxInitialDeffendPoint;

  [Header("Player config")]
  public int InitialExpCap;

  [Header("Enemy config")]
  public int MinExpGain;
  public int MaxExpGain;
  public int MinGoldGain;
  public int MaxGoldGain;

  public int GetInitialHealth()
  {
    return Random.Range(MinInitialHealth, MaxInitialHealth + 1);
  }

  public int GetInitialAttackPoint()
  {
    return Random.Range(MinInitialAttackPoint, MaxInitialAttackPoint + 1);
  }

  public int GetInitialDeffendPoint()
  {
    return Random.Range(MinInitialDeffendPoint, MaxInitialDeffendPoint + 1);
  }

  public int GetExpGain()
  {
    return Random.Range(MinExpGain, MaxExpGain + 1);
  }
}
