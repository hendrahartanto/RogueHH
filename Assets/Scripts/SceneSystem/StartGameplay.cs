using UnityEngine;

public class StartGameplay : MonoBehaviour
{
  [SerializeField] private DungeonSO _dungeonSO = default;
  [SerializeField] SceneSO _sceneToLoad = default;
  [SerializeField] SceneSO _secondarySceneToLoad = default;
  [SerializeField] private bool _showLoadingScreen = default;

  [Header("Broadcasting on")]
  [SerializeField] private LoadEventChannelSO _loadLocationEvent = default;
  [SerializeField] private VoidEventChannelSO _setDungeonConfigEvent = default;

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
    if (_dungeonSO.CurrentLevel == -1)
      _loadLocationEvent.RaiseEvent(_secondarySceneToLoad, _showLoadingScreen);
    else
    {
      _setDungeonConfigEvent.RaiseEvent();
      _loadLocationEvent.RaiseEvent(_sceneToLoad, _showLoadingScreen);
    }
  }
}
