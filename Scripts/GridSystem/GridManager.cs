using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
  [SerializeField] private GridTileSO _grid = default;
  [SerializeField] private GridNodeSO _gridNode = default;

  [Header("Listening to")]
  [SerializeField] private ChangeCellTypeEventChanel _changeCellTypeEvent = default;

  private void OnEnable()
  {
    _changeCellTypeEvent.OnEventRaised += ChangeCellType;
  }

  private void OnDisable()
  {
    _changeCellTypeEvent.OnEventRaised -= ChangeCellType;
  }

  private void ChangeCellType(int x, int z, CellType cellType)
  {
    _grid[x, z].cellTypes.Clear();
    _grid[x, z].cellTypes.Add(cellType);
  }
}
