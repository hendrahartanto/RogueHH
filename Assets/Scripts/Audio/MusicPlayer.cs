using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
  [SerializeField] private SceneSO _thisSceneSO = default;
  [SerializeField] private AudioConfigSO _audioConfig = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;

  [Header("Broadcasting to")]
  [SerializeField] private AudioCueEventChannelSO _musicEvent = default;

  private void OnEnable()
  {
    _onSceneReady.OnEventRaised += PlayMusic;
  }

  private void OnDisable()
  {
    _onSceneReady.OnEventRaised -= PlayMusic;
  }

  private void PlayMusic()
  {
    _musicEvent.RaisePlayEvent(_thisSceneSO.musicTrack, _audioConfig);
  }
}
