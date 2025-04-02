using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackOnClick", menuName = "StateMachine/Actions/Pointer/AttackOnClick")]
public class AttackOnClickActionSO : StateActionSO
{
  public InputReader InputReader = default;

  [Header("Broadcasting to")]
  public BoolEventChannelSO IsTurnCyclingSetActiveEvent = default;
  protected override StateAction CreateAction() => new AttackOnClickAction(InputReader, IsTurnCyclingSetActiveEvent);
}

public class AttackOnClickAction : StateAction
{
  private PointerManager _pointerManager;
  private InputReader _inputReader;
  private Attack _attack;

  private BoolEventChannelSO _isTurnCyclingSetActiveEvent = default;

  public AttackOnClickAction(InputReader inputReader, BoolEventChannelSO isTurnCyclingSetActiveEvent)
  {
    _inputReader = inputReader;
    _isTurnCyclingSetActiveEvent = isTurnCyclingSetActiveEvent;
  }

  public override void Awake(StateMachine stateMachine)
  {
    _pointerManager = stateMachine.GetComponent<PointerManager>();
    _attack = stateMachine.GetComponent<Attack>();
  }

  public override void OnStateEnter()
  {
    _inputReader.MouseClickEvent += AttackTarget;
  }

  public override void OnStateExit()
  {
    _inputReader.MouseClickEvent -= AttackTarget;
  }

  private void AttackTarget()
  {
    _isTurnCyclingSetActiveEvent.RaiseEvent(true);

    Collider target = _pointerManager.CurrentPointedCollider;
    if (target.TryGetComponent(out Damagable damagableComp))
    {
      _attack.AttacTarget(damagableComp);
    }
  }

  public override void OnUpdate() { }
}
