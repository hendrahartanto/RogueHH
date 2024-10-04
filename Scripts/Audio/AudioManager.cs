using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  [SerializeField] private SoundEmitterPoolSO _pool = default;
  [SerializeField] private int _initialSize = 10;
  private SoundEmitterVault _soundEmitterVault;
  private SoundEmitter _musicSoundEmitter;

  [Header("Listening on")]
  [SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;

  private void Awake()
  {
    _soundEmitterVault = new SoundEmitterVault();

    _pool.Prewarm(_initialSize);
    _pool.SetParent(this.transform);
  }

  private void OnEnable()
  {
    _musicEventChannel.OnAudioCuePlayRequested += PlayMusicTrack;
    _musicEventChannel.OnAudioCueStopRequested += StopMusic;
  }

  private void OnDisable()
  {
    _musicEventChannel.OnAudioCuePlayRequested -= PlayMusicTrack;
    _musicEventChannel.OnAudioCueStopRequested -= StopMusic;
  }

  private AudioCueKey PlayMusicTrack(AudioCueSO audioCue, AudioConfigSO audioConfiguration, Vector3 positionInSpace)
  {
    float fadeDuration = 2f;
    float startTime = 0f;

    if (_musicSoundEmitter != null && _musicSoundEmitter.IsPlaying())
    {
      AudioClip songToPlay = audioCue.GetClips()[0];
      if (_musicSoundEmitter.GetClip() == songToPlay)
        return AudioCueKey.Invalid;

      //Music is already playing, need to fade it out
      startTime = _musicSoundEmitter.FadeMusicOut(fadeDuration);
    }

    _musicSoundEmitter = _pool.Request();
    _musicSoundEmitter.FadeMusicIn(audioCue.GetClips()[0], audioConfiguration, 1f, startTime);
    _musicSoundEmitter.OnSoundFinishedPlaying += StopMusicEmitter;

    return AudioCueKey.Invalid; //No need to return a valid key for music
  }

  private bool StopMusic(AudioCueKey key)
  {
    if (_musicSoundEmitter != null && _musicSoundEmitter.IsPlaying())
    {
      _musicSoundEmitter.Stop();
      return true;
    }
    else
      return false;
  }

  private void StopMusicEmitter(SoundEmitter soundEmitter)
  {
    soundEmitter.OnSoundFinishedPlaying -= StopMusicEmitter;
    _pool.Return(soundEmitter);
  }
}
