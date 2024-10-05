using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAudio : MonoBehaviour
{
  [SerializeField] protected AudioCueEventChannelSO _sfxEventChannel = default;
  [SerializeField] protected AudioConfigSO _audioConfig = default;
  public bool IsCriticalHit = false;
  private void ToggleIsCriticalHit() => IsCriticalHit = true;

  protected void PlayAudio(AudioCueSO audioCue, AudioConfigSO audioConfig, Vector3 positionInSpace = default)
  {
    _sfxEventChannel.RaisePlayEvent(audioCue, audioConfig, positionInSpace);
  }

  [Header("Combat")]
  [SerializeField] private AudioCueSO _swordSwing;
  [SerializeField] private AudioCueSO _swordSlash;
  [SerializeField] private AudioCueSO _punch;
  [SerializeField] private AudioCueSO _criticalHit;
  [SerializeField] private AudioCueSO _footStep;

  //called by animation event
  public void PlaySwordSwing() => PlayAudio(_swordSwing, _audioConfig, transform.position);
  public void PlaySwordSlash()
  {
    if (IsCriticalHit)
    {
      PlayAudio(_criticalHit, _audioConfig, transform.position);
      IsCriticalHit = false;
    }

    PlayAudio(_swordSlash, _audioConfig, transform.position);
  }
  public void PlayPunch()
  {
    if (IsCriticalHit)
    {
      PlayAudio(_criticalHit, _audioConfig, transform.position);
      IsCriticalHit = false;
    }

    PlayAudio(_punch, _audioConfig, transform.position);
  }
  public void PlayFootStep() => PlayAudio(_footStep, _audioConfig, transform.position);

}
