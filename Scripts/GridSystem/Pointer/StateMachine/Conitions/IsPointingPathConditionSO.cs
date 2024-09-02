using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsPointingPath", menuName = "StateMachine/Conditions/Pointer/IsPointingPath")]
public class IsPointingPathConditionSO : StateConditionSO
{
  private PointerManager _pointerManager;

  public override void InitComponent(StateMachine stateMachine)
  {
    _pointerManager = stateMachine.GetComponent<PointerManager>();
  }

  protected override bool Statement()
  {
    // Debug.Log("Restricted");
    GridTileSO grid = _pointerManager.Grid;
    Vector3Int gridPosition = _pointerManager.GridPosition;

    return _pointerManager.isPointingNull ? false : grid[gridPosition.x, gridPosition.y].cellTypes.Contains(CellType.Walkable);
  }

}
