using System;
using UnityEngine;
using UnityEngine.Events;

public class SkillSO : ScriptableObject
{
  public String Name;
  public Sprite SkillIcon;
  public int UnlockLevel;
  public int Index;
  public int CooldownTime;
  public int CurrentCooldownTime = -1;
  public int ActiveTime;
  public int CurrentActiveTime = -1;
  public bool IsSelected = false;

  [Header("Broadcasting to")]
  public IntIntEventChannelSO UpdateSkillCooldownUIEvent = default;

  [Header("Listening on")]
  public VoidEventChannelSO OnTurnCycleExecuted = default;

  private void OnEnable()
  {
    OnTurnCycleExecuted.OnEventRaised += OnTurnFinished;
  }

  private void OnDisable()
  {
    OnTurnCycleExecuted.OnEventRaised -= OnTurnFinished;
  }

  public virtual void Setup(GameObject parent) { }
  public virtual void Activate() { }
  public virtual void Deactivate() { }

  public void OnSkillSelected()
  {
    IsSelected = !IsSelected;
  }

  private void OnTurnFinished()
  {
    CurrentCooldownTime--;
    CurrentActiveTime--;

    if (CurrentCooldownTime < 0) CurrentCooldownTime = -1;
    if (CurrentActiveTime < 0) CurrentActiveTime = -1;

    if (CurrentActiveTime == 0)
    {
      Deactivate();

      //TODO: hilangkan indicator buff diatas
    }

    UpdateSkillCooldownUIEvent.RaiseEvent(Index, CurrentCooldownTime);
  }
}
