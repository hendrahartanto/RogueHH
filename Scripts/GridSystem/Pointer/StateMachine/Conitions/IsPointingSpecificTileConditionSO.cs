using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsPointingSpecificTile", menuName = "StateMachine/Conditions/Pointer/IsPointingSpecificTile")]
public class IsPointingSpecificTileConditionSO : StateConditionSO
{
  public CellType CellTypeToCheck;
  protected override Condition CreateCondition() => new IsPointingSpecificTileCondition(CellTypeToCheck);
}

public class IsPointingSpecificTileCondition : Condition
{
  private PointerManager _pointerManager;
  private CellType _cellTypeToCheck;

  public IsPointingSpecificTileCondition(CellType cellTypeToCheck)
  {
    _cellTypeToCheck = cellTypeToCheck;
  }
  public override void Awake(StateMachine stateMachine)
  {
    _pointerManager = stateMachine.GetComponent<PointerManager>();
  }

  protected override bool Statement()
  {
    GridTileSO grid = _pointerManager.Grid;
    Vector3Int gridPosition = _pointerManager.GridPosition;

    bool pointingNullOutcome = _cellTypeToCheck == CellType.Restricted ? true : false;

    return _pointerManager.isPointingNull ? pointingNullOutcome : grid[gridPosition.x, gridPosition.y].cellTypes.Contains(_cellTypeToCheck);
  }
}
