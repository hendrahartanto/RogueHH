using UnityEngine;

[CreateAssetMenu(menuName = "Event Channels/AudioCueEventChannel")]
public class AudioCueEventChannelSO : ScriptableObject
{
  public AudioCuePlayAction OnAudioCuePlayRequested;
  public AudioCueStopAction OnAudioCueStopRequested;
  public AudioCueFinishAction OnAudioCueFinishRequested;

  public AudioCueKey RaisePlayEvent(AudioCueSO audioCue, AudioConfigSO audioConfiguration, Vector3 positionInSpace = default)
  {
    AudioCueKey audioCueKey = AudioCueKey.Invalid;

    if (OnAudioCuePlayRequested != null)
    {
      audioCueKey = OnAudioCuePlayRequested.Invoke(audioCue, audioConfiguration, positionInSpace);
    }
    else
    {
      Debug.LogWarning("request dari " + audioCue.name + " tapi ga ada yang merespon");
    }

    return audioCueKey;
  }

  public bool RaiseStopEvent(AudioCueKey audioCueKey)
  {
    bool requestSucceed = false;

    if (OnAudioCueStopRequested != null)
    {
      requestSucceed = OnAudioCueStopRequested.Invoke(audioCueKey);
    }
    else
    {
      Debug.LogWarning("stop audio que di request tapi gaada yang merespon");
    }

    return requestSucceed;
  }

  public bool RaiseFinishEvent(AudioCueKey audioCueKey)
  {
    bool requestSucceed = false;

    if (OnAudioCueStopRequested != null)
    {
      requestSucceed = OnAudioCueFinishRequested.Invoke(audioCueKey);
    }
    else
    {
      Debug.LogWarning("finish audio que di request tapi gaada yang merespon");
    }

    return requestSucceed;
  }
}

public delegate AudioCueKey AudioCuePlayAction(AudioCueSO audioCue, AudioConfigSO audioConfiguration, Vector3 positionInSpace);
public delegate bool AudioCueStopAction(AudioCueKey emitterKey);
public delegate bool AudioCueFinishAction(AudioCueKey emitterKey);
