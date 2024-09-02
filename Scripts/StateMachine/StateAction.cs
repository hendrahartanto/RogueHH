using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAction : IState
{
  public StateActionSO OriginSO;

  public abstract void OnUpdate();

  public virtual void Awake(StateMachine stateMachine) { }

  public virtual void OnStateEnter() { }

  public virtual void OnStateExit() { }
}
