using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Exp", menuName = "Gameplay/Character/Exp")]
public class ExpSO : ScriptableObject
{
  [SerializeField] private int _expCap;
  private int _currentExp;

  public int ExpCap => _expCap;
  public int CurrentExp => _currentExp;

  private void Start()
  {
    _expCap = 5;
    _currentExp = 0;
  }

  public void Reset()
  {
    _expCap = 5;
    _currentExp = 0;
  }

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
