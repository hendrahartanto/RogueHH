using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Attack : MonoBehaviour
{
  private Damagable _currentTarget;
  public bool IsAttacking = false;

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
      //TODO: set using configSO
      _currentTarget.ReceiveAttack(transform, 5);
    }
  }
}
