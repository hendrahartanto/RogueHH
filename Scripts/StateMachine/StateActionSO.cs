using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateActionSO : ScriptableObject, IState
{
  public abstract void OnUpdate();
  public virtual void InitComponent(StateMachine stateMachine) { }
  public virtual void OnStateEnter() { }
  public virtual void OnStateExit() { }
}
