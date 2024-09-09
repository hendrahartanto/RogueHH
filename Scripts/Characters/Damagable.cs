using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
  [SerializeField] private HealthSO _currentHealth;

  public bool IsGettingHit = false;
  public bool IsDead = false;
  private Transform _source;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _onTurnCycleExecuted = default;
  [SerializeField] private VoidEventChannelSO _onTurnFinished = default;
  [SerializeField] private TurnComponentEventChanelSO _removeEnemyFromQueueEvent = default;
  [SerializeField] private ChangeCellTypeEventChanel _changeCellTypeEvent = default;
  [SerializeField] private GridNodeBoolEventChanelSO _changeNodeAccessibleEvent = default;
  [SerializeField] private VoidEventChannelSO _recalculatePathEvent = default;
  [SerializeField] private Vector3_Int_EvetChanel _damagePopUpEvent = default;
  public IntEventChanelSO SetMaxHealthUIEvent = default;
  public IntEventChanelSO UpdateHealthUIEvent = default;

  private void Start()
  {
    if (_currentHealth == null)
    {
      _currentHealth = ScriptableObject.CreateInstance<HealthSO>();
    }

    //TODO: set it using config SO
    _currentHealth.SetMaxHealth(100);
    _currentHealth.SetCurrentHealth(100);

    SetMaxHealthUIEvent.RaiseEvent(_currentHealth.MaxHealth);
  }

  public void ReceiveAttack(Transform source, int damage)
  {
    _source = source;

    RotateTowardsTarget(source);

    _currentHealth.DecreaseHealth(damage);

    UpdateHealthUIEvent.RaiseEvent(_currentHealth.CurrentHealth);

    _damagePopUpEvent.RaiseEvent(transform.position, damage);

    IsGettingHit = true;

    if (_currentHealth.CurrentHealth <= 0)
    {
      IsDead = true;
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

  //called by animation event
  private void StopGettingHit()
  {
    IsGettingHit = false;
  }

  private void FinishTurn()
  {
    if (_source.gameObject.CompareTag("Player"))
    {
      IsDead = false;
      _changeCellTypeEvent.RaiseEvent((int)transform.position.x / GridConfig.CellSize.x, (int)transform.position.z / GridConfig.CellSize.z, CellType.Walkable);
      _changeNodeAccessibleEvent.RaiseEvent((int)transform.position.x / GridConfig.CellSize.x, (int)transform.position.z / GridConfig.CellSize.z, true);
      _recalculatePathEvent.RaiseEvent();

      if (_removeEnemyFromQueueEvent != null)
      {
        _removeEnemyFromQueueEvent.RaiseEvent(GetComponent<Enemy>());
      }

      _onTurnCycleExecuted.RaiseEvent();
      Destroy(gameObject);
    }
  }
}
