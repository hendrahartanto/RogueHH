using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfigSO", menuName = "Configs/Character/CharacterConfigSO")]
public class CharacterConfigSO : ScriptableObject
{
  public int MinInitialHealth;
  public int MaxInitialHealth;
  public int MinInitialAttackPoint;
  public int MaxInitialAttackPoint;
  public int MinInitialDeffendPoint;
  public int MaxInitialDeffendPoint;

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
}
