using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
  private int _expCap = default;

  [SerializeField] private ExpSO _currentExp = default;
  [SerializeField] private CharacterConfigSO _characterConfigSO = default;

  [Header("Broadcastin to")]
  [SerializeField] private IntEventChanelSO _updateExpUIEvent = default;
  [SerializeField] private IntEventChanelSO _setupExpUIEvent = default;
  [SerializeField] private VoidEventChannelSO _playerLevelUpEvent = default;

  [Header("Listening to")]
  [SerializeField] private IntEventChanelSO _gainExpEvent = default;

  private void Awake()
  {
    _expCap = _characterConfigSO.InitialExpCap;
  }

  private void Start()
  {
    _currentExp.SetExpCap(_expCap);
    _currentExp.SetCurrentExp(0);
    _setupExpUIEvent.RaiseEvent(_expCap);
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

    if (_currentExp.CurrentExp >= _expCap)
    {
      int remainingExp = _currentExp.CurrentExp - _expCap;
      _characterConfigSO.Level++;

      _playerLevelUpEvent.RaiseEvent();
      SetupStats(remainingExp);
    }

    _updateExpUIEvent.RaiseEvent(_currentExp.CurrentExp);
  }

  public void SetupStats(int remainingExp)
  {
    _expCap = (int)(_expCap * Math.Pow(_characterConfigSO.Level + 1, 1.2));

    _currentExp.SetExpCap(_expCap);
    _currentExp.SetCurrentExp(remainingExp);

    _setupExpUIEvent.RaiseEvent(_expCap);
    _updateExpUIEvent.RaiseEvent(remainingExp);
  }
}
