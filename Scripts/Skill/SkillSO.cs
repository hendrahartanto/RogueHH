using System;
using UnityEngine;
using UnityEngine.Events;

public class SkillSO : ScriptableObject
{
  public String Name;
  public Sprite SkillIcon;
  public String Description;
  public int UnlockLevel;
  public int Index;
  public int CooldownTime;
  public int CurrentCooldownTime = -1;
  public int ActiveTime;
  public int CurrentActiveTime = -1;
  public bool IsSelected = false;

  [Header("Broadcasting to")]
  public IntIntEventChannelSO UpdateSkillCooldownUIEvent = default;
  public UpdateUIIndicatorChannelSO UpdateUIIndicator = default;

  [Header("Listening on")]
  public VoidEventChannelSO OnTurnCycleExecuted = default;
  public VoidEventChannelSO ReduceSkillTimeEvent = default;

  private void OnEnable()
  {
    OnTurnCycleExecuted.OnEventRaised += OnTurnFinished;
    ReduceSkillTimeEvent.OnEventRaised += OnTurnFinished;
  }

  private void OnDisable()
  {
    OnTurnCycleExecuted.OnEventRaised -= OnTurnFinished;
    ReduceSkillTimeEvent.OnEventRaised -= OnTurnFinished;
  }

  private void OnDestroy()
  {
    OnTurnCycleExecuted.OnEventRaised -= OnTurnFinished;
    ReduceSkillTimeEvent.OnEventRaised -= OnTurnFinished;
  }

  public virtual void Setup(GameObject parent) { }
  public virtual void SetupDescription() { }
  public virtual void Activate() { }
  public virtual void Deactivate() { }
  public virtual void ExecuteSkillAction() { }

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
    }

    if (CurrentActiveTime > -1)
      UpdateUIIndicator?.RaiseEvent(this, Index);

    UpdateSkillCooldownUIEvent.RaiseEvent(Index, CurrentCooldownTime);
  }
}
