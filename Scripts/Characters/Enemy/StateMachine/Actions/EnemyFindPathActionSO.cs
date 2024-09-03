using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFindPathAction", menuName = "StateMachine/Actions/Enemy/EnemyFindPathAction")]

public class EnemyFindPathActionSO : StateActionSO
{
  public TransformAnchorSO PlayerTransform;
  public GridNodeSO GridNode;
  protected override StateAction CreateAction() => new EnemyFindPathAction();
}

public class EnemyFindPathAction : StateAction
{
  private AStar _aStar;
  private PathStorageSO _pathStorage;
  private Enemy _enemy;
  private Transform _enemyTransform;
  private TransformAnchorSO _playerTransform;
  private GridNodeSO _gridNode;

  public override void Awake(StateMachine stateMachine)
  {
    _playerTransform = ((EnemyFindPathActionSO)OriginSO).PlayerTransform;
    _gridNode = ((EnemyFindPathActionSO)OriginSO).GridNode;
    _enemy = stateMachine.GetComponent<Enemy>();
    _pathStorage = _enemy.PathStorage;
    _enemyTransform = stateMachine.transform;
    _aStar = new AStar();
  }

  public override void OnStateEnter()
  {
    Node startNode = _gridNode[(int)_enemyTransform.position.x / GridConfig.CellSize.x, (int)_enemyTransform.position.z / GridConfig.CellSize.z];
    Node endNode = _gridNode[(int)_playerTransform.Value.position.x / GridConfig.CellSize.x, (int)_playerTransform.Value.position.z / GridConfig.CellSize.z];

    _aStar.FindPath(_gridNode, _pathStorage, startNode, endNode);
    foreach (var path in _pathStorage.paths)
    {
      Debug.Log(path.Position.x + " " + path.Position.z);
    }
  }

  public override void OnStateExit()
  {
  }

  public override void OnUpdate() { }
}
