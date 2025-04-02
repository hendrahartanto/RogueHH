using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon", menuName = "Dungeon/Config")]
public class DungeonSO : ScriptableObject
{
  public Vector2Int Size;
  public int RoomCount;
  public List<Room> rooms = new List<Room>();
  public int CurrentLevel = 0;
  public int MaxLevelReached = 0;

  public void Reset()
  {
    CurrentLevel = 0;
    MaxLevelReached = 0;
  }
}
