using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFindPathActionSO : StateActionSO
{
  private AStar _aStar;
  private PathStorageSO _pathStorage;
  private Enemy _enemy;

  public override void InitComponent(StateMachine stateMachine)
  {
    _enemy = stateMachine.GetComponent<Enemy>();
    _pathStorage = _enemy.PathStorage;
  }
  public override void OnUpdate()
  {
  }
}
