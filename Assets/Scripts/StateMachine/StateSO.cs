using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "StateMachine/State")]
public class StateSO : ScriptableObject
{
  [SerializeField] private StateActionSO[] _actions = null;
  internal State GetState(StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
  {
    if (createdInstances.TryGetValue(this, out var obj))
      return (State)obj;

    var state = new State();
    createdInstances.Add(this, state);

    state.OriginSO = this;
    state.StateMachine = stateMachine;
    state.Transitions = new StateTransition[0];
    state.Actions = GetActions(_actions, stateMachine, createdInstances);

    return state;
  }

  private static StateAction[] GetActions(StateActionSO[] scriptableActions,
    StateMachine stateMachine, Dictionary<ScriptableObject, object> createdInstances)
  {
    int count = scriptableActions.Length;
    var actions = new StateAction[count];
    for (int i = 0; i < count; i++)
      actions[i] = scriptableActions[i].GetAction(stateMachine, createdInstances);

    return actions;
  }
}
