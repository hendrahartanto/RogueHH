using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "StateMachine/State")]
public class StateSO : ScriptableObject, IState
{
  public StateTransition[] Transitions;
  public StateActionSO[] Actions;

  public void InitComponent(StateMachine stateMachine)
  {
    for (int i = 0; i < Transitions.Length; i++)
      Transitions[i].InitComponent(stateMachine);

    for (int i = 0; i < Actions.Length; i++)
      Actions[i].InitComponent(stateMachine);
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

  public bool GetTransition(out StateSO state)
  {
    state = null;
    for (int i = 0; i < Transitions.Length; i++)
      if (Transitions[i].GetTransition(out state))
        break;

    for (int i = 0; i < Transitions.Length; i++)
      Transitions[i].ClearConditionsCache();

    return state != null;
  }
}
