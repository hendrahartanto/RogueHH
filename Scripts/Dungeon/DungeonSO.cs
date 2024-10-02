using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon", menuName = "Dungeon/Config")]
public class DungeonSO : ScriptableObject
{
  public Vector2Int Size;
  public int RoomCount;
  public List<Room> rooms = new List<Room>();
  public int Level = 0;
}
