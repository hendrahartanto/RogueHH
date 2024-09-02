using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Dungeon/Room")]
public class RoomSO : ScriptableObject
{
  public Vector2Int roomMaxSize;
  public GameObject floorPrefab;
  public List<DecorationSO> possibleDecorations;
  public List<int> chances;

  public DecorationSO ChooseRandomDecoration()
  {
    int totalChance = 0;
    foreach (int chance in chances)
    {
      totalChance += chance;
    }

    int randomValue = Random.Range(0, totalChance);
    int cumulativeChance = 0;

    for (int i = 0; i < possibleDecorations.Count; i++)
    {
      cumulativeChance += chances[i];
      if (randomValue < cumulativeChance)
      {
        return possibleDecorations[i];
      }
    }

    return null;
  }
}
