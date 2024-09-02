using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class StateConditionSO : ScriptableObject, IState
{
  private bool _isCached = false;
  private bool _cachedStatement = default;
  protected abstract bool Statement();

  public bool GetStatement()
  {
    if (!_isCached)
    {
      _cachedStatement = Statement();
      _isCached = true;
    }

    return _cachedStatement;
  }

  public void ClearStatementCache()
  {
    _isCached = false;
  }

  public virtual void InitComponent(StateMachine stateMachine) { }
  public virtual void OnStateEnter() { }
  public virtual void OnStateExit() { }
}
