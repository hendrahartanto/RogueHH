using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Attack : MonoBehaviour
{
  [SerializeField] private CharacterConfigSO _characterConfigSO = default;
  private int _attackPoint = default;
  private int _weaponAttackPoint = default;
  private Damagable _currentTarget;
  public bool IsAttacking = false;

  private void Awake()
  {
    //TODO: total attack = base attack + weapon damage
    _attackPoint = _characterConfigSO.GetInitialAttackPoint();

    //TODO: assign dengan weapon attack point setelah ada fitur weapon
    _weaponAttackPoint = 0;
  }

  public void AttacTarget(Damagable target)
  {
    StartCoroutine(RotateTowardsTarget(target.transform));
    IsAttacking = true;
    _currentTarget = target;
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

  //called by animation event
  private void StopAttacking()
  {
    IsAttacking = false;
  }

  private void TriggerAttackEvent()
  {
    if (_currentTarget != null)
    {
      int effectiveDamage = Calculation.CalculateDamage(_attackPoint, _weaponAttackPoint, _currentTarget.DeffendPoint);
      _currentTarget.ReceiveAttack(transform, effectiveDamage);
    }
  }
}
