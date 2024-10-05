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
  [SerializeField] private AudioCueEventChannelSO _sfxEventChannel = default;

  private void Awake()
  {
    _soundEmitterVault = new SoundEmitterVault();

    _pool.Prewarm(_initialSize);
    _pool.SetParent(this.transform);
  }

  private void OnEnable()
  {
    _sfxEventChannel.OnAudioCuePlayRequested += PlayAudioCue;
    _sfxEventChannel.OnAudioCueStopRequested += StopAudioCue;
    _sfxEventChannel.OnAudioCueFinishRequested += FinishAudioCue;

    _musicEventChannel.OnAudioCuePlayRequested += PlayMusicTrack;
    _musicEventChannel.OnAudioCueStopRequested += StopMusic;
  }

  private void OnDisable()
  {
    _sfxEventChannel.OnAudioCuePlayRequested -= PlayAudioCue;
    _sfxEventChannel.OnAudioCueStopRequested -= StopAudioCue;
    _sfxEventChannel.OnAudioCueFinishRequested -= FinishAudioCue;

    _musicEventChannel.OnAudioCuePlayRequested -= PlayMusicTrack;
    _musicEventChannel.OnAudioCueStopRequested -= StopMusic;
  }

  public AudioCueKey PlayAudioCue(AudioCueSO audioCue, AudioConfigSO settings, Vector3 position = default)
  {
    Debug.Log("PLAY SFX");
    AudioClip[] clipsToPlay = audioCue.GetClips();
    SoundEmitter[] soundEmitterArray = new SoundEmitter[clipsToPlay.Length];

    int nOfClips = clipsToPlay.Length;

    for (int i = 0; i < nOfClips; i++)
    {
      soundEmitterArray[i] = _pool.Request();
      if (soundEmitterArray[i] != null)
      {
        soundEmitterArray[i].PlayAudioClip(clipsToPlay[i], settings, audioCue.looping, position);
        if (!audioCue.looping)
          soundEmitterArray[i].OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
      }
    }

    return _soundEmitterVault.Add(audioCue, soundEmitterArray);
  }

  public bool FinishAudioCue(AudioCueKey audioCueKey)
  {
    bool isFound = _soundEmitterVault.Get(audioCueKey, out SoundEmitter[] soundEmitters);

    if (isFound)
    {
      for (int i = 0; i < soundEmitters.Length; i++)
      {
        soundEmitters[i].Finish();
        soundEmitters[i].OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
      }
    }
    else
    {
      Debug.LogWarning("Finishing an AudioCue was requested, but the AudioCue was not found.");
    }

    return isFound;
  }

  public bool StopAudioCue(AudioCueKey audioCueKey)
  {
    bool isFound = _soundEmitterVault.Get(audioCueKey, out SoundEmitter[] soundEmitters);

    if (isFound)
    {
      for (int i = 0; i < soundEmitters.Length; i++)
      {
        StopAndCleanEmitter(soundEmitters[i]);
      }

      _soundEmitterVault.Remove(audioCueKey);
    }

    return isFound;
  }

  private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
  {
    StopAndCleanEmitter(soundEmitter);
  }

  private void StopAndCleanEmitter(SoundEmitter soundEmitter)
  {
    if (!soundEmitter.IsLooping())
      soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;

    soundEmitter.Stop();
    _pool.Return(soundEmitter);

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

      startTime = _musicSoundEmitter.FadeMusicOut(fadeDuration);
    }

    _musicSoundEmitter = _pool.Request();
    _musicSoundEmitter.FadeMusicIn(audioCue.GetClips()[0], audioConfiguration, 1f, startTime);
    _musicSoundEmitter.OnSoundFinishedPlaying += StopMusicEmitter;

    return AudioCueKey.Invalid;
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
