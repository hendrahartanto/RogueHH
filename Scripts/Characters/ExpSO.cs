using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Exp", menuName = "Gameplay/Character/Exp")]
public class ExpSO : ScriptableObject
{
  private int _expCap;
  private int _currentExp;

  public int ExpCap => _expCap;
  public int CurrentExp => _currentExp;

  public void SetExpCap(int expCapValue)
  {
    _expCap = expCapValue;
  }

  public void SetCurrentExp(int currentExpValue)
  {
    _currentExp = currentExpValue;
  }

  public void GainExp(int expValue)
  {
    _currentExp += expValue;
  }
}
