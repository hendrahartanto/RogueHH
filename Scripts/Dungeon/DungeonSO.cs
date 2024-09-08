using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon", menuName = "Dungeon/Config")]
public class DungeonSO : ScriptableObject
{
  public Vector2Int Size;
  public int RoomCount;
  public List<Room> rooms = new List<Room>();
  public List<EnemyBaseSO> PossibleEnemyTypes = new List<EnemyBaseSO>();
  public List<int> EnemyTypeChances;


  public EnemyBaseSO GetRandomEnemyType()
  {
    int totalChance = 0;
    foreach (int chance in EnemyTypeChances)
    {
      totalChance += chance;
    }

    int randomValue = Random.Range(0, totalChance);
    int cumulativeChance = 0;

    for (int i = 0; i < PossibleEnemyTypes.Count; i++)
    {
      cumulativeChance += EnemyTypeChances[i];
      if (randomValue < cumulativeChance)
      {
        return PossibleEnemyTypes[i];
      }
    }

    return null;
  }
}
