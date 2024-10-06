using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFloorIndicator : MonoBehaviour
{
  [SerializeField] private DungeonSO _dungeonSO;
  [SerializeField] private TextMeshProUGUI _text = default;

  [Header("Listening on")]
  [SerializeField] private IntEventChanelSO _updateFloorIndicatorUIEvent = default;

  private void Awake()
  {
    SetText(_dungeonSO.CurrentLevel);
  }

  private void OnEnable()
  {
    _updateFloorIndicatorUIEvent.OnEventRaised += SetText;
  }

  private void OnDisable()
  {
    _updateFloorIndicatorUIEvent.OnEventRaised -= SetText;
  }

  private void SetText(int currentFloor)
  {
    _text.SetText("Floor " + (currentFloor + 1).ToString());
  }
}
