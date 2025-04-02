using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
  [SerializeField] SceneSO _sceneToLoad = default;
  [SerializeField] private bool _showLoadingScreen = default;

  [Header("Broadcasting on")]
  [SerializeField] private LoadEventChannelSO _loadLocation = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _startNewGameEvent = default;

  private void OnEnable()
  {
    _startNewGameEvent.OnEventRaised += StartNewGame;
  }

  private void OnDisable()
  {
    _startNewGameEvent.OnEventRaised -= StartNewGame;
  }

  private void StartNewGame()
  {
    _loadLocation.RaiseEvent(_sceneToLoad, _showLoadingScreen);
  }
}
