using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class StateTransition : IState
{
  [SerializeField] private StateSO _targetState;
  [SerializeField] private StateConditionSO[] _conditions;
  [SerializeField] private bool[] _expectedResults;
  [SerializeField] private StateConditionOperator _conditionOperator;
  private List<bool> _results;

  private void OnEnable()
  {
    _results = new List<bool>();
  }

  public bool GetTransition(out StateSO state)
  {
    _results.Clear();

    state = null;
    for (int i = 0; i < _conditions.Length; i++)
      _results.Add(_conditions[i].GetStatement() == _expectedResults[i]);

    if (_conditionOperator == StateConditionOperator.AND)
      state = _results.All(element => element) ? _targetState : null;
    else
      state = _results.Any(element => element) ? _targetState : null;

    return state != null;
  }

  public void OnStateEnter()
  {
    for (int i = 0; i < _conditions.Length; i++)
      _conditions[i].OnStateEnter();
  }

  public void OnStateExit()
  {
    for (int i = 0; i < _conditions.Length; i++)
      _conditions[i].OnStateExit();
  }

  public void ClearConditionsCache()
  {
    for (int i = 0; i < _conditions.Length; i++)
      _conditions[i].ClearStatementCache();
  }

  public void InitComponent(StateMachine stateMachine)
  {
    for (int i = 0; i < _conditions.Length; i++)
      _conditions[i].InitComponent(stateMachine);
  }
}

public enum StateConditionOperator
{
  AND,
  OR
}