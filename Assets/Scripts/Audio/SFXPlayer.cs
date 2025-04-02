using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
  public List<SFXMapping> SFXList = new List<SFXMapping>();
  public AudioConfigSO SFXConfig = default;

  [Header("Listening on")]
  [SerializeField] private PlaySFXEventChannelSO _playSFXEvent = default;

  [Header("Broadcasting to")]
  [SerializeField] private AudioCueEventChannelSO _sfxEvent = default;

  private void OnEnable()
  {
    _playSFXEvent.OnEventRaised += PlaySFX;
    _playSFXEvent.OnStopEventRaised += StopSFX;
  }

  private void OnDisable()
  {
    _playSFXEvent.OnEventRaised -= PlaySFX;
    _playSFXEvent.OnStopEventRaised -= StopSFX;
  }

  public AudioCueSO GetAudioCue(SFXName sfxName)
  {
    foreach (var mapping in SFXList)
    {
      if (mapping.SFXKey == sfxName)
      {
        return mapping.SFXValue;
      }
    }

    Debug.LogWarning($"SFX with name {sfxName} not found!");
    return null;
  }

  public void SetAudioCueKey(SFXName sfxName, AudioCueKey key)
  {
    foreach (var mapping in SFXList)
    {
      if (mapping.SFXKey == sfxName)
      {
        mapping.SFXCueKey = key;
      }
    }
  }

  public AudioCueKey GetAudioCueKey(SFXName sfxName)
  {
    foreach (var mapping in SFXList)
    {
      if (mapping.SFXKey == sfxName)
      {
        return mapping.SFXCueKey;
      }
    }

    return SFXList[0].SFXCueKey;
  }

  private void PlaySFX(SFXName sfxName, Transform transform)
  {
    AudioCueKey key = _sfxEvent.RaisePlayEvent(GetAudioCue(sfxName), SFXConfig, transform.position);
    SetAudioCueKey(sfxName, key);
  }

  private void StopSFX(SFXName sfxName)
  {
    _sfxEvent.RaiseStopEvent(GetAudioCueKey(sfxName));
  }

}

[Serializable]
public class SFXMapping
{
  public SFXName SFXKey;
  public AudioCueSO SFXValue;
  public AudioCueKey SFXCueKey;
}

public enum SFXName
{
  Alert,
  MenuSelect,
  MenuClick,
  MenuOpen,
  MenuClosed,
  Purchase,
  Campfire,
  CheatActive
}
