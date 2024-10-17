using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ini sebenarnya bisa jadi go to specific location scene cs (tapi malas refactor nama)
public class BackToUpgradeMenu : MonoBehaviour
{
  [SerializeField] SceneSO _sceneToLoad = default;
  [SerializeField] private bool _showLoadingScreen = default;

  [Header("Broadcasting on")]
  [SerializeField] private LoadEventChannelSO _loadLocation = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _backToUpgradeMenuEvent = default;

  private void OnEnable()
  {
    _backToUpgradeMenuEvent.OnEventRaised += GoToUpgradeMenu;
  }

  private void OnDisable()
  {
    _backToUpgradeMenuEvent.OnEventRaised -= GoToUpgradeMenu;
  }

  private void GoToUpgradeMenu()
  {
    _loadLocation.RaiseEvent(_sceneToLoad, _showLoadingScreen);
  }
}
