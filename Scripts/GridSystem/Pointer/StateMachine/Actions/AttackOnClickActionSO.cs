using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackOnClick", menuName = "StateMachine/Actions/Pointer/AttackOnClick")]
public class AttackOnClickActionSO : StateActionSO
{
  public InputReader InputReader = default;
  protected override StateAction CreateAction() => new AttackOnClickAction(InputReader);
}

public class AttackOnClickAction : StateAction
{
  private PointerManager _pointerManager;
  private InputReader _inputReader;
  private Attack _attack;

  public AttackOnClickAction(InputReader inputReader)
  {
    _inputReader = inputReader;
  }

  public override void Awake(StateMachine stateMachine)
  {
    _pointerManager = stateMachine.GetComponent<PointerManager>();
    _attack = stateMachine.GetComponent<Attack>();
  }

  public override void OnStateEnter()
  {
    Debug.Log("Attack on click enter");
    _inputReader.MouseClickEvent += AttackTarget;
  }

  public override void OnStateExit()
  {
    _inputReader.MouseClickEvent -= AttackTarget;
  }

  private void AttackTarget()
  {
    Collider target = _pointerManager.CurrentPointedCollider;
    if (target.TryGetComponent(out Damagable damagableComp))
    {
      _attack.AttacTarget(damagableComp);
    }
  }

  public override void OnUpdate() { }
}
