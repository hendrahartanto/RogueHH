using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gold", menuName = "Gameplay/Character/Gold")]
public class GoldSO : ScriptableObject
{
  private int _gold;

  public int Gold => _gold;

  public void SetGold(int value)
  {
    _gold = value;
  }

  public void IncreaseGold(int value)
  {
    _gold += value;
  }
}
