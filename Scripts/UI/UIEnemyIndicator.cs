using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnemyIndicator : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _text = default;
  private int _currentEnemyCount = default;

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
    _text.SetText(currentEnemyCount.ToString() + " Enemy left");
    _currentEnemyCount = currentEnemyCount;
  }

  private void DecreaseEnemyCount()
  {
    _currentEnemyCount--;
    _text.SetText(_currentEnemyCount.ToString() + " Enemy left");
  }
}
