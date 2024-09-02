using UnityEngine;

public static class GridConfig
{
  public static readonly Vector3Int CellSize = new Vector3Int(2, 0, 2);

  public static readonly Vector3 Offset = new Vector3(CellSize.x / 2, 0, CellSize.z / 2);
}