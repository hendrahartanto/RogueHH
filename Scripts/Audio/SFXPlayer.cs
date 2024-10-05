using System;
using System.Collections;
using System.Collections.Generic;
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
  }

  private void OnDisable()
  {
    _playSFXEvent.OnEventRaised -= PlaySFX;
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

  private void PlaySFX(SFXName sfxName, Transform transform)
  {
    _sfxEvent.RaisePlayEvent(GetAudioCue(sfxName), SFXConfig, transform.position);
  }

}

[Serializable]
public class SFXMapping
{
  public SFXName SFXKey;
  public AudioCueSO SFXValue;
}

public enum SFXName
{
  Alert,
  MenuSelect,
  MenuClick,
  Purchase
}
