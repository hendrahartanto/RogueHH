using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : IState
{
  public StateSO OriginSO;
  public StateMachine StateMachine;
  public StateAction[] Actions;
  public StateTransition[] Transitions;

  public State() { }

  public State(
    StateSO originSO,
    StateMachine stateMachine,
    StateTransition[] transitions,
    StateAction[] actions)
  {
    OriginSO = originSO;
    StateMachine = stateMachine;
    Transitions = transitions;
    Actions = actions;
  }

  public void OnStateEnter()
  {
    void OnStateEnter(IState[] comps)
    {
      for (int i = 0; i < comps.Length; i++)
        comps[i].OnStateEnter();
    }
    OnStateEnter(Transitions);
    OnStateEnter(Actions);
  }

  public void OnUpdate()
  {
    for (int i = 0; i < Actions.Length; i++)
      Actions[i].OnUpdate();
  }

  public void OnStateExit()
  {
    void OnStateExit(IState[] comps)
    {
      for (int i = 0; i < comps.Length; i++)
        comps[i].OnStateExit();
    }
    OnStateExit(Transitions);
    OnStateExit(Actions);
  }

  public bool TryGetTransition(out State state)
  {
    state = null;

    for (int i = 0; i < Transitions.Length; i++)
      if (Transitions[i].TryGetTransiton(out state))
        break;

    for (int i = 0; i < Transitions.Length; i++)
      Transitions[i].ClearConditionsCache();

    return state != null;
  }
}
