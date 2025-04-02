using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
  [SerializeField] private GoldSO _goldSO = default;

  [Header("Listening to")]
  [SerializeField] private IntEventChanelSO _gainGoldEvent = default;

  [Header("Broadcasting on")]
  [SerializeField] private IntEventChanelSO _updateGoldIndicatorUIEvent = default;

  private void OnEnable()
  {
    _gainGoldEvent.OnEventRaised += GainGold;
  }

  private void OnDisable()
  {
    _gainGoldEvent.OnEventRaised -= GainGold;
  }

  private void GainGold(int goldValue)
  {
    _goldSO.IncreaseGold(goldValue);
    _updateGoldIndicatorUIEvent.RaiseEvent(_goldSO.CurrentGold);
  }

}
