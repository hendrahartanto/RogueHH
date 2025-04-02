using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DisableRaycast", menuName = "StateMachine/Actions/Pointer/DisableRaycast")]
public class DisableRaycastActionSO : StateActionSO<DisableRaycastAction> { }

public class DisableRaycastAction : StateAction
{
  private PointerManager _pointerManager;

  public override void Awake(StateMachine stateMachine)
  {
    _pointerManager = stateMachine.GetComponent<PointerManager>();
  }

  public override void OnStateEnter()
  {
    _pointerManager.enabled = false;
  }

  public override void OnUpdate() { }
}
