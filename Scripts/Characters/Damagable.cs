using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
  [SerializeField] private HealthSO _currentHealth;

  public bool IsGettingHit = false;
  public bool isDead = false;
  private Transform _source;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _onTurnCycleExecuted = default;
  [SerializeField] private VoidEventChannelSO _onTurnFinished = default;


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
    _source = source;

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

  private void CheckTurnCycleTrigger()
  {
    //jika source attack dari player maka turn cycle akan berjalan
    if (_source.gameObject.CompareTag("Player"))
    {
      _onTurnCycleExecuted.RaiseEvent();
    }

    //kalo source attack dari enemy maka enemy akan notify bahwa turn dia sudah selesai
    if (_source.gameObject.CompareTag("Enemy"))
    {
      _onTurnFinished.RaiseEvent();
    }
  }
}
