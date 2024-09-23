using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameplay : MonoBehaviour
{
  [SerializeField] SceneSO _sceneToLoad = default;
  [SerializeField] private bool _showLoadingScreen = default;

  [Header("Broadcasting on")]
  [SerializeField] private LoadEventChannelSO _loadLocationEvent = default;

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
    _loadLocationEvent.RaiseEvent(_sceneToLoad, _showLoadingScreen);
  }
}
