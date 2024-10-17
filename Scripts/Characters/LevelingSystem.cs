using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
  [SerializeField] private ExpSO _currentExp = default;
  [SerializeField] private CharacterConfigSO _characterConfigSO = default;

  [Header("Broadcastin to")]
  [SerializeField] private IntEventChanelSO _updateExpUIEvent = default;
  [SerializeField] private IntEventChanelSO _setupExpUIEvent = default;
  [SerializeField] private VoidEventChannelSO _playerLevelUpEvent = default;
  [SerializeField] private TextPopupEventChannelSO _textPopupEvent = default;
  [SerializeField] private IntEventChanelSO _checkSkillTobeUnlockedEvent = default;

  [Header("Listening to")]
  [SerializeField] private IntEventChanelSO _gainExpEvent = default;

  private void Start()
  {
    _currentExp.SetExpCap(_currentExp.ExpCap);
    _currentExp.SetCurrentExp(_currentExp.CurrentExp);
    _setupExpUIEvent.RaiseEvent(_currentExp.ExpCap);
    _updateExpUIEvent.RaiseEvent(_currentExp.CurrentExp);
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

    if (_currentExp.CurrentExp >= _currentExp.ExpCap)
    {
      OnLevelUp();
    }

    _updateExpUIEvent.RaiseEvent(_currentExp.CurrentExp);
  }

  public void SetupStats(int remainingExp)
  {
    _currentExp.SetExpCap((int)(_currentExp.ExpCap + 5 + Mathf.Pow(_characterConfigSO.Level, (float)1.5)));

    _currentExp.SetExpCap(_currentExp.ExpCap);
    _currentExp.SetCurrentExp(remainingExp);

    _setupExpUIEvent.RaiseEvent(_currentExp.ExpCap);
    _updateExpUIEvent.RaiseEvent(remainingExp);
  }

  private void OnLevelUp()
  {
    //popup text
    _textPopupEvent.RaiseEvent(transform.position, "Level up!", TextColor.Yellow);

    int remainingExp = _currentExp.CurrentExp;

    while (remainingExp >= _currentExp.ExpCap)
    {
      Debug.Log("exp cap" + _currentExp.ExpCap);
      Debug.Log("remaining exp" + remainingExp);

      remainingExp -= _currentExp.ExpCap;

      _characterConfigSO.Level++;

      _playerLevelUpEvent.RaiseEvent();

      _checkSkillTobeUnlockedEvent.RaiseEvent(_characterConfigSO.Level);

      SetupStats(remainingExp);

      Debug.Log("remaining exp after" + remainingExp);
      Debug.Log("exp cap after" + _currentExp.ExpCap);
    }
  }
}
