using UnityEngine;

[CreateAssetMenu(fileName = "StopPlayerMovementOnClickAction", menuName = "StateMachine/Actions/Pointer/StopPlayerMovementOnClickAction")]
public class StopPlayerMovementOnClickActionSO : StateActionSO
{
  [SerializeField] InputReader _inputReader = default;
  private Player _player;

  public override void InitComponent(StateMachine stateMachine)
  {
    _player = stateMachine.GetComponent<Player>();
  }

  public override void OnStateEnter()
  {
    _inputReader.MouseClickEvent += NotifyStopMoving;
  }

  public override void OnStateExit()
  {
    _inputReader.MouseClickEvent -= NotifyStopMoving;
  }

  public void NotifyStopMoving()
  {
    _player.OnNotifyStopMoving();
  }

  public override void OnUpdate() { }
}
