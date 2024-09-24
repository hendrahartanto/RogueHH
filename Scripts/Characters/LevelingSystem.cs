using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
  [SerializeField] private ExpSO _currentExp = default;
  [SerializeField] private CharacterConfigSO _characterConfigSO = default;

  [Header("Broadcastin to")]
  [SerializeField] private IntEventChanelSO _updateExpUIEvent = default;
  [SerializeField] private IntEventChanelSO _setupExpUIEvent = default;

  [Header("Listening to")]
  [SerializeField] private IntEventChanelSO _gainExpEvent = default;

  private void Start()
  {
    _currentExp.SetExpCap(_characterConfigSO.InitialExpCap);
    _currentExp.SetCurrentExp(0);
    _setupExpUIEvent.RaiseEvent(_characterConfigSO.InitialExpCap);
    _updateExpUIEvent.RaiseEvent(0);
  }

  private void OnEnable()
  {
    _gainExpEvent.OnEventRaised += GainExp;
  }

  private void OnDisable()
  {
    _gainExpEvent.OnEventRaised -= GainExp;
  }

  private void GainExp(int expValue)
  {
    _currentExp.GainExp(expValue);
    _updateExpUIEvent.RaiseEvent(_currentExp.CurrentExp);
  }
}
