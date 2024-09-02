using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseSO : ScriptableObject
{
  public GameObject Prefab;
  public int MaxHealth;
  public int AttackPoint;
  public int DefendPoint;
}
