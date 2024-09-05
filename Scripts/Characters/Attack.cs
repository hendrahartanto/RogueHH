using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
  public bool IsAttacking = false;

  public void AttacTarget(Damagable target)
  {
    StartCoroutine(RotateTowardsTarget(target.transform));

    StartAttacking();
    //TODO: set using configSO
    target.ReceiveAttack(5);
  }

  private IEnumerator RotateTowardsTarget(Transform targetTransform)
  {
    Debug.Log("COROUTINE CALLED");
    // Calculate the direction from the current object to the target
    Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

    //rotate
    if (directionToTarget != Vector3.zero)
    {
      Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

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
  private void StartAttacking()
  {
    Debug.Log("Start Attacking");
    IsAttacking = true;
  }
  private void StopAttacking()
  {
    Debug.Log("Stop Attacking");
    IsAttacking = false;
  }
}
