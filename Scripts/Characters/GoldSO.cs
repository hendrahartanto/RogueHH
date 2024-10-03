using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gold", menuName = "Gameplay/Character/Gold")]
public class GoldSO : ScriptableObject
{
  [SerializeField] private int _gold;
  public int CurrentGold => _gold;

  public void SetGold(int value)
  {
    _gold = value;
  }

  public void IncreaseGold(int value)
  {
    _gold += value;
  }

  public void DecreaseGold(int value)
  {
    _gold -= value;
  }
}
