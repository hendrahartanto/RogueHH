using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayCanvasManager : MonoBehaviour
{
  private Canvas _gameplayCanvas = default;

  [Header("Listening to")]
  [SerializeField] private BoolEventChannelSO _setGameplayCanvasActiveEvent;

  private void Awake()
  {
    _gameplayCanvas = GetComponent<Canvas>();
  }

  private void OnEnable()
  {
    _setGameplayCanvasActiveEvent.OnEventRaised += SetCanvasActive;
  }

  private void OnDisable()
  {
    _setGameplayCanvasActiveEvent.OnEventRaised -= SetCanvasActive;
  }

  public void SetCanvasActive(bool isActive)
  {
    _gameplayCanvas.enabled = isActive;
  }
}
