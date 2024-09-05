using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
  [SerializeField] private HealthSO _currentHealth;

  public bool IsGettingHit = false;
  public bool isDead = false;

  private void Awake()
  {
    if (_currentHealth == null)
    {
      _currentHealth = ScriptableObject.CreateInstance<HealthSO>();
    }

    //TODO: set it using config SO
    _currentHealth.SetMaxHealth(100);
    _currentHealth.SetCurrentHealth(100);
  }

  public void ReceiveAttack(Transform source, int damage)
  {
    RotateTowardsTarget(source);

    _currentHealth.DecreaseHealth(damage);

    IsGettingHit = true;

    if (_currentHealth.CurrentHealth <= 0)
    {
      isDead = true;
    }
  }

  private void RotateTowardsTarget(Transform targetTransform)
  {
    Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

    if (directionToTarget != Vector3.zero)
    {
      Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

      transform.rotation = targetRotation;
    }
  }

  //called by animation event
  private void StopGettingHit()
  {
    IsGettingHit = false;
  }
}
