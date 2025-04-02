using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnemyIndicator : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _text = default;
  private int _currentEnemyCount = default;

  [Header("Broadcasting to")]
  [SerializeField] private BoolEventChannelSO _floorClearedModalSetActiveEvent = default;
  [SerializeField] private GameStateEventChanelSO _changeGameStateEvent = default;

  [Header("Listening on")]
  [SerializeField] private IntEventChanelSO _updateEnemyIndicatorUIEvent = default;

  [SerializeField] private VoidEventChannelSO _decreaseEnemyCountEvent = default;

  private void OnEnable()
  {
    _updateEnemyIndicatorUIEvent.OnEventRaised += SetText;
    _decreaseEnemyCountEvent.OnEventRaised += DecreaseEnemyCount;
  }

  private void OnDisable()
  {
    _updateEnemyIndicatorUIEvent.OnEventRaised -= SetText;
    _decreaseEnemyCountEvent.OnEventRaised -= DecreaseEnemyCount;
  }

  private void SetText(int currentEnemyCount)
  {
    _text.SetText(" Enemy left: " + currentEnemyCount.ToString());
    _currentEnemyCount = currentEnemyCount;
  }

  private void DecreaseEnemyCount()
  {
    _currentEnemyCount--;
    _text.SetText(" Enemy left: " + _currentEnemyCount.ToString());

    //saat floor udah cleared
    if (_currentEnemyCount <= 0)
    {
      _floorClearedModalSetActiveEvent.RaiseEvent(true);
      _changeGameStateEvent?.RaiseEvent(GameState.Gameover);
    }
  }
}
