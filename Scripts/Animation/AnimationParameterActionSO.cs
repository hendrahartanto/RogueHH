using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationParameter", menuName = "StateMachine/Actions/AnimationParameter")]
public class AnimationParameterActionSO : StateActionSO
{
  public ParameterType parameterType = default;
  public string parameterName = default;

  public bool boolValue = default;
  public int intValue = default;
  public float floatValue = default;
  public SpecificMoment whenToRun = default;

  protected override StateAction CreateAction() => new AnimatorParameterAction(Animator.StringToHash(parameterName));

  public enum ParameterType
  {
    Bool, Int, Float, Trigger,
  }
}

public class AnimatorParameterAction : StateAction
{
  private Animator _animator;
  private AnimationParameterActionSO _originSO => (AnimationParameterActionSO)base.OriginSO;
  private int _parameterHash;

  public AnimatorParameterAction(int parameterHash)
  {
    _parameterHash = parameterHash;
  }

  public override void Awake(StateMachine stateMachine)
  {
    _animator = stateMachine.GetComponent<Animator>();
  }

  public override void OnStateEnter()
  {
    if (_originSO.whenToRun == SpecificMoment.OnStateEnter)
      SetParameter();
  }

  public override void OnStateExit()
  {
    if (_originSO.whenToRun == SpecificMoment.OnStateExit)
      SetParameter();
  }

  private void SetParameter()
  {
    switch (_originSO.parameterType)
    {
      case AnimationParameterActionSO.ParameterType.Bool:
        _animator.SetBool(_parameterHash, _originSO.boolValue);
        break;
      case AnimationParameterActionSO.ParameterType.Int:
        _animator.SetInteger(_parameterHash, _originSO.intValue);
        break;
      case AnimationParameterActionSO.ParameterType.Float:
        _animator.SetFloat(_parameterHash, _originSO.floatValue);
        break;
      case AnimationParameterActionSO.ParameterType.Trigger:
        _animator.SetTrigger(_parameterHash);
        break;
    }
  }

  public override void OnUpdate() { }
}
