using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusicPlayer : MonoBehaviour
{
  public AudioCueSO BattleThemeMusic = default;
  public AudioConfigSO MusicConfig = default;
  public SceneSO DungeonScene = default;
  private bool _isPlayingBattleMusic = false;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _playStopBattleMusicEvent = default;
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;

  [Header("Broadcasting to")]
  [SerializeField] private AudioCueEventChannelSO _musicEvent = default;

  private void OnEnable()
  {
    _playStopBattleMusicEvent.OnEventRaised += PlayStopBattleMusic;
    _onSceneReady.OnEventRaised += Reset;
  }

  private void OnDisable()
  {
    _playStopBattleMusicEvent.OnEventRaised -= PlayStopBattleMusic;
    _onSceneReady.OnEventRaised -= Reset;
  }

  private void Reset()
  {
    _isPlayingBattleMusic = false;
  }

  private void PlayStopBattleMusic()
  {
    if (!_isPlayingBattleMusic)
    {
      _musicEvent.RaisePlayEvent(BattleThemeMusic, MusicConfig);
      _isPlayingBattleMusic = true;
    }
    else
    {
      _musicEvent.RaisePlayEvent(DungeonScene.musicTrack, MusicConfig);
      _isPlayingBattleMusic = false;
    }
  }
}
