using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoSpecificMenuScene : MonoBehaviour
{
  [SerializeField] SceneSO _sceneToLoad = default;
  [SerializeField] private bool _showLoadingScreen = default;

  [Header("Broadcasting to")]
  [SerializeField] private LoadEventChannelSO _loadMenu = default;
  [SerializeField] private VoidEventChannelSO _removeAllTurnQueueEvent = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _loadToSpecificMenuEvent = default;

  private void OnEnable()
  {
    _loadToSpecificMenuEvent.OnEventRaised += GotoSpecificMenu;
  }

  private void OnDisable()
  {
    _loadToSpecificMenuEvent.OnEventRaised -= GotoSpecificMenu;
  }

  private void GotoSpecificMenu()
  {
    _removeAllTurnQueueEvent?.RaiseEvent();
    _loadMenu.RaiseEvent(_sceneToLoad, _showLoadingScreen);
  }
}
