using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : IState
{
  private bool _isCached = false;
  private bool _cachedStatement = default;
  public StateConditionSO OriginSO;
  protected abstract bool Statement();

  public bool GetStatement()
  {
    if (!_isCached)
    {
      _isCached = true;
      _cachedStatement = Statement();
    }

    return _cachedStatement;
  }

  public void ClearStatementCache()
  {
    _isCached = false;
  }

  public virtual void Awake(StateMachine stateMachine) { }
  public virtual void OnStateEnter() { }
  public virtual void OnStateExit() { }
}

public readonly struct StateCondition
{
  public readonly StateMachine _stateMachine;
  public readonly Condition _condition;
  public readonly bool _expectedResult;

  public StateCondition(StateMachine stateMachine, Condition condition, bool expectedResult)
  {
    _stateMachine = stateMachine;
    _condition = condition;
    _expectedResult = expectedResult;
  }

  public bool IsMet()
  {
    bool statement = _condition.GetStatement();
    bool isMet = statement == _expectedResult;

    // #if UNITY_EDITOR
    // 			_stateMachine._debugger.TransitionConditionResult(_condition._originSO.name, statement, isMet);
    // #endif
    return isMet;
  }
}
