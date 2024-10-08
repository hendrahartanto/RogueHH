using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Attack : MonoBehaviour
{
  [SerializeField] private CharacterConfigSO _characterConfigSO = default;
  [SerializeField] private GameObject _swordObject = default;
  [SerializeField] private GameObject _trail = default;
  private HumanAudio _humanAudio = default;
  public int _attackPoint = default;
  public float SkillMultiplier = 0f;
  public bool IsSkillActive = false;
  public bool IsCastingSkill = false;
  private float _criticalRate = default;
  private float _criticalDamage = default;
  private int _weaponAttackPoint = default;
  private Damagable _currentTarget;
  public bool IsAttacking = false;
  private bool _isCriticalHit = false;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _cameraShakeEvent = default;
  [SerializeField] private VoidEventChannelSO _buffSkillActionEvent = default;
  [SerializeField] private VoidEventChannelSO _followUpSkillActionEvent = default;
  [SerializeField] private VoidEventChannelSO _activeSkillActionEvent = default;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _playerLevelUpEvent = default;

  private void Awake()
  {
    _humanAudio = GetComponent<HumanAudio>();
    SetupStats();
    SetWeaponType();
  }

  private void OnEnable()
  {
    if (_playerLevelUpEvent != null)
      _playerLevelUpEvent.OnEventRaised += SetupStats;
  }

  private void OnDisable()
  {
    if (_playerLevelUpEvent != null)
      _playerLevelUpEvent.OnEventRaised -= SetupStats;
  }

  public void SetupStats()
  {
    //TODO: total attack = base attack + weapon damage
    _attackPoint = _characterConfigSO.GetInitialAttackPoint();

    _criticalRate = _characterConfigSO.CriticalRate;
    _criticalDamage = _characterConfigSO.CriticalDamage;

    //TODO: assign dengan weapon attack point setelah ada fitur weapon
    _weaponAttackPoint = 0;
  }

  public void AttacTarget(Damagable target)
  {
    StartCoroutine(RotateTowardsTarget(target.transform));

    //trigger state and animation
    if (IsSkillActive)
    {
      _activeSkillActionEvent?.RaiseEvent();
      IsCastingSkill = true;
    }
    else
      IsAttacking = true;

    _currentTarget = target;

    if ((_isCriticalHit = IsCriticalHit()) == true)
      _humanAudio.IsCriticalHit = true;
  }

  private IEnumerator RotateTowardsTarget(Transform targetTransform)
  {
    Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

    //rotate
    if (directionToTarget != Vector3.zero)
    {
      Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

      //TODO: set using configSO
      float rotationSpeed = 50f;
      while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
      {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        yield return null;
      }

      transform.rotation = targetRotation;
    }
  }

  private bool IsCriticalHit()
  {
    return Random.value <= _criticalRate;
  }

  //called by animation event
  private void StopAttacking()
  {
    IsAttacking = false;
  }

  private void StopCastingSkill()
  {
    IsCastingSkill = false;
  }

  private void TriggerAttackEvent()
  {
    if (_currentTarget != null)
    {
      bool critical = false;

      int effectiveDamage = Calculation.CalculateDamage(IsSkillActive ? _attackPoint * SkillMultiplier : _attackPoint, _weaponAttackPoint, _currentTarget.DeffendPoint);

      if (_isCriticalHit)
      {
        _cameraShakeEvent.RaiseEvent();
        critical = true;
        effectiveDamage = Mathf.RoundToInt(effectiveDamage * _criticalDamage);
      }

      _currentTarget.ReceiveAttack(transform, effectiveDamage, critical);

      //jika ada followup action maka ketrigger
      _followUpSkillActionEvent?.RaiseEvent();
    }
  }

  private void ShowSword()
  {
    _swordObject?.SetActive(true);
  }

  private void HideSword()
  {
    _swordObject?.SetActive(false);
  }

  private void SetWeaponType()
  {
    GetComponent<Animator>().SetInteger("WeaponType", _characterConfigSO.WeaponType);
  }

  private void ShowTrail()
  {
    _trail?.SetActive(true);
  }

  private void HideTrail()
  {
    _trail?.SetActive(false);
  }
}
