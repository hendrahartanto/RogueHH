using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkillChecker : MonoBehaviour
{
  [SerializeField] private CharacterConfigSO _characterConfigSO = default;

  [Header("Broadcasting to")]
  [SerializeField] private IntEventChanelSO _checkSkillTobeUnlockedEvent = default;

  private void Start()
  {
    _checkSkillTobeUnlockedEvent.RaiseEvent(_characterConfigSO.Level);
  }
}
