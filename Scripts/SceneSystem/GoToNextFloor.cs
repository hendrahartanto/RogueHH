using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextFloor : MonoBehaviour
{
  [SerializeField] DungeonSO _dungeonSO = default;
  [SerializeField] SceneSO _sceneToLoad = default;
  [SerializeField] private bool _showLoadingScreen = default;

  [Header("Broadcasting on")]
  [SerializeField] private LoadEventChannelSO _loadLocationEvent = default;
  [SerializeField] private IntEventChanelSO _updateFloorIndicatorUIEvent = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _startGameplayEvent = default;

  private void OnEnable()
  {
    _startGameplayEvent.OnEventRaised += StartNewGame;
  }

  private void OnDisable()
  {
    _startGameplayEvent.OnEventRaised -= StartNewGame;
  }

  private void StartNewGame()
  {
    _dungeonSO.CurrentLevel++;

    if (_dungeonSO.CurrentLevel > _dungeonSO.MaxLevelReached)
      _dungeonSO.MaxLevelReached = _dungeonSO.CurrentLevel;

    _updateFloorIndicatorUIEvent.RaiseEvent(_dungeonSO.CurrentLevel);
    _dungeonSO.rooms.Clear();

    _loadLocationEvent.RaiseEvent(_sceneToLoad, _showLoadingScreen);
  }
}
