using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Dungeon/Room")]
public class RoomSO : ScriptableObject
{
  public Vector2Int roomMaxSize;
  public Vector2Int roomMinSize;

  public List<GameObject> possibleFloors;
  public List<int> FloorChances;
  public List<DecorationSO> possibleDecorations;
  public List<int> DecorationChances;

  public DecorationSO ChooseRandomDecoration()
  {
    int totalChance = 0;
    foreach (int chance in DecorationChances)
    {
      totalChance += chance;
    }

    int randomValue = Random.Range(0, totalChance);
    int cumulativeChance = 0;

    for (int i = 0; i < possibleDecorations.Count; i++)
    {
      cumulativeChance += DecorationChances[i];
      if (randomValue < cumulativeChance)
      {
        return possibleDecorations[i];
      }
    }

    return null;
  }

  public GameObject ChooseRandomFloor()
  {
    int totalChance = 0;
    foreach (int chance in FloorChances)
    {
      totalChance += chance;
    }

    int randomValue = Random.Range(0, totalChance);
    int cumulativeChance = 0;

    for (int i = 0; i < possibleFloors.Count; i++)
    {
      cumulativeChance += FloorChances[i];
      if (randomValue < cumulativeChance)
      {
        return possibleFloors[i];
      }
    }

    return null;
  }
}
