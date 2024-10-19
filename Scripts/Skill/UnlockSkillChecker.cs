using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkillChecker : MonoBehaviour
{
  [SerializeField] private CharacterConfigSO _characterConfigSO = default;

  [Header("Listening on")]
  [SerializeField] private VoidEventChannelSO _onCheatExecutedEvent = default;

  [Header("Broadcasting to")]
  [SerializeField] private IntEventChanelSO _checkSkillTobeUnlockedEvent = default;

  private void OnEnable()
  {
    _onCheatExecutedEvent.OnEventRaised += Check;
  }

  private void OnDisable()
  {
    _onCheatExecutedEvent.OnEventRaised -= Check;
  }

  private void Start()
  {
    Check();
  }

  private void Check() => _checkSkillTobeUnlockedEvent.RaiseEvent(_characterConfigSO.Level);
}
