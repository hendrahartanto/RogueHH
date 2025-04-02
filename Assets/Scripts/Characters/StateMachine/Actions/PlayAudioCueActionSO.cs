using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayAudioCue", menuName = "StateMachine/Actions/Character/PlayAudioCue")]
public class PlayAudioCueActionSO : StateActionSO<PlayAudioCueAction>
{
  public AudioCueSO AudioCue = default;
  public AudioCueEventChannelSO AudioCueEventChannel = default;
  public AudioConfigSO AudioConfig = default;
}

public class PlayAudioCueAction : StateAction
{
  private Transform _stateMachineTransform;
  private PlayAudioCueActionSO _originSO => (PlayAudioCueActionSO)base.OriginSO;

  public override void Awake(StateMachine stateMachine)
  {
    _stateMachineTransform = stateMachine.transform;
  }

  public override void OnStateEnter()
  {
    _originSO.AudioCueEventChannel.RaisePlayEvent(_originSO.AudioCue, _originSO.AudioConfig, _stateMachineTransform.position);
  }

  public override void OnUpdate() { }
}


