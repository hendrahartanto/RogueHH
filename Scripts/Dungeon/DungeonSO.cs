using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon", menuName = "Dungeon/Config")]
public class DungeonSO : ScriptableObject
{
  public Vector2Int Size;
  public int RoomCount;
  public List<Room> rooms = new List<Room>();
  public List<EnemyBaseSO> possibleEnemies = new List<EnemyBaseSO>();

  public EnemyBaseSO GetRandomEnemy()
  {
    //TODO: konsiderasi ada chance per tipe enemy
    return possibleEnemies[GlobalRandom.Next(0, possibleEnemies.Count)];
  }
}
