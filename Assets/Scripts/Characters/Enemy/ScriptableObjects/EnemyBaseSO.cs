using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseSO : ScriptableObject
{
  public List<GameObject> PrefabVariants;

  public GameObject GetRandomPrefab()
  {
    return PrefabVariants[Random.Range(0, PrefabVariants.Count)];
  }
}
