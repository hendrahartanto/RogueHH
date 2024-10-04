using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
  private AudioSource _audioSource;

  public event UnityAction<SoundEmitter> OnSoundFinishedPlaying;

  private void Awake()
  {
    _audioSource = this.GetComponent<AudioSource>();
    _audioSource.playOnAwake = false;
  }

  public void PlayAudioClip(AudioClip clip, AudioConfigSO settings, bool hasToLoop, Vector3 position = default)
  {
    _audioSource.clip = clip;
    settings.ApplyTo(_audioSource);
    _audioSource.transform.position = position;
    _audioSource.loop = hasToLoop;
    _audioSource.time = 0f;
    _audioSource.Play();

    if (!hasToLoop)
    {
      StartCoroutine(FinishedPlaying(clip.length));
    }
  }

  public void FadeMusicIn(AudioClip musicClip, AudioConfigSO settings, float duration, float startTime = 0f)
  {
    PlayAudioClip(musicClip, settings, true);

    _audioSource.volume = 0f;

    if (startTime <= _audioSource.clip.length)
      _audioSource.time = startTime;

    StartCoroutine(FadeInCoroutine(duration, settings.Volume));
  }

  public float FadeMusicOut(float duration)
  {
    StartCoroutine(FadeOutCoroutine(duration));

    return _audioSource.time;
  }

  public IEnumerator FadeInCoroutine(float duration, float targetVolume)
  {
    float currentTime = 0f;
    float startVolume = 0f;
    _audioSource.volume = startVolume;


    while (currentTime < duration)
    {
      currentTime += Time.deltaTime;
      _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
      yield return null;
    }
  }

  public IEnumerator FadeOutCoroutine(float duration)
  {
    float currentTime = 0f;
    float startVolume = _audioSource.volume;

    while (currentTime < duration)
    {
      currentTime += Time.deltaTime;
      _audioSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
      yield return null;
    }

    OnFadeOutComplete();
  }

  private void OnFadeOutComplete()
  {
    NotifyBeingDone();
  }

  public AudioClip GetClip()
  {
    return _audioSource.clip;
  }


  public void Resume()
  {
    _audioSource.Play();
  }

  public void Pause()
  {
    _audioSource.Pause();
  }

  public void Stop()
  {
    _audioSource.Stop();
  }

  public void Finish()
  {
    if (_audioSource.loop)
    {
      _audioSource.loop = false;
      float timeRemaining = _audioSource.clip.length - _audioSource.time;
      StartCoroutine(FinishedPlaying(timeRemaining));
    }
  }

  public bool IsPlaying()
  {
    return _audioSource.isPlaying;
  }

  public bool IsLooping()
  {
    return _audioSource.loop;
  }

  IEnumerator FinishedPlaying(float clipLength)
  {
    yield return new WaitForSeconds(clipLength);

    NotifyBeingDone();
  }

  private void NotifyBeingDone()
  {
    Debug.Log("Finished playing");
    OnSoundFinishedPlaying.Invoke(this);
  }
}
