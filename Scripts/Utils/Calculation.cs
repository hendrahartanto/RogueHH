using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculation
{
  public static int CalculateDamage(float baseAttack, float weaponDamage, float defence)
  {
    float defenceScalingFactor = 50f; //scaling factor mendetermine impact defence terhadap damage reduction

    float totalAttack = baseAttack + weaponDamage;
    float defenceFactor = 1 - (defence / (defence + defenceScalingFactor));
    float effectiveDamage = totalAttack * defenceFactor;

    float randomFactor = UnityEngine.Random.Range(0.9f, 1.1f); // +-10%

    effectiveDamage *= randomFactor;

    return (int)Mathf.Max(0, effectiveDamage);
  }
}
