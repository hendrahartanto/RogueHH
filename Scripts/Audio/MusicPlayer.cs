using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;
  [SerializeField] private AudioCueEventChannelSO _musicEvent = default;
  [SerializeField] private SceneSO _thisSceneSO = default;
  [SerializeField] private AudioConfigSO _audioConfig = default;

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
    Debug.Log("Play musiic");
    _musicEvent.RaisePlayEvent(_thisSceneSO.musicTrack, _audioConfig);
  }
}
