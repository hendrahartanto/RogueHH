using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "isPointingRestricted", menuName = "StateMachine/Conditions/Pointer/IsPointingRestricted")]

public class IsPointingRestrictedConditionSO : StateConditionSO
{
  private PointerManager _pointerManager;

  public override void InitComponent(StateMachine stateMachine)
  {
    _pointerManager = stateMachine.GetComponent<PointerManager>();
  }

  protected override bool Statement()
  {
    // Debug.Log("Path");
    GridTileSO grid = _pointerManager.Grid;
    Vector3Int gridPosition = _pointerManager.GridPosition;

    return _pointerManager.isPointingNull ? true : !grid[gridPosition.x, gridPosition.y].cellTypes.Contains(CellType.Walkable);
  }
}
