using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
  [SerializeField] private HealthSO _currentHealth;

  public bool IsGettingHit = false;
  public bool IsDead = false;
  private Transform _source;
  [NonSerialized] public int DeffendPoint = default;

  [SerializeField] private CharacterConfigSO _characterConfigSO = default;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _onTurnCycleExecuted = default;
  [SerializeField] private VoidEventChannelSO _onTurnFinished = default;
  [SerializeField] private TurnComponentEventChanelSO _removeEnemyFromQueueEvent = default;
  [SerializeField] private ChangeCellTypeEventChanel _changeCellTypeEvent = default;
  [SerializeField] private GridNodeBoolEventChanelSO _changeNodeAccessibleEvent = default;
  [SerializeField] private VoidEventChannelSO _recalculatePathEvent = default;
  [SerializeField] private DamagePopupEventChannel _damagePopUpEvent = default;
  [SerializeField] private IntEventChanelSO _gainExpEvent = default;
  [SerializeField] private IntEventChanelSO _gainGoldEvent = default;
  [SerializeField] private BoolEventChannelSO _gameOverModalSetActiveEvent = default;

  public IntEventChanelSO SetMaxHealthUIEvent = default;
  public IntEventChanelSO UpdateHealthUIEvent = default;

  private void Start()
  {
    if (_currentHealth == null)
    {
      _currentHealth = ScriptableObject.CreateInstance<HealthSO>();
    }

    _currentHealth.SetMaxHealth(_characterConfigSO.GetInitialHealth());
    _currentHealth.SetCurrentHealth(_currentHealth.MaxHealth);

    //setup initial defend point in the SO container
    DeffendPoint = _characterConfigSO.GetInitialDeffendPoint();

    SetMaxHealthUIEvent.RaiseEvent(_currentHealth.MaxHealth);
  }

  public void ReceiveAttack(Transform source, int damage, bool critical)
  {
    _source = source;

    RotateTowardsTarget(source);

    _currentHealth.DecreaseHealth(damage);

    UpdateHealthUIEvent.RaiseEvent(_currentHealth.CurrentHealth);

    _damagePopUpEvent.RaiseEvent(transform.position, damage, critical);

    IsGettingHit = true;

    if (_currentHealth.CurrentHealth <= 0)
    {
      if (gameObject.CompareTag("Player"))
        OnPlayerDeath();
      else if (gameObject.CompareTag("Enemy"))
      {
        //TODO: set isdeadnya untuk player juga
        IsDead = true;
        OnEnemyDeath();
      }

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

  private void Despawn()
  {
    if (_source.gameObject.CompareTag("Player"))
    {
      IsDead = false;
      Destroy(gameObject);
    }
  }

  private void OnEnemyDeath()
  {
    GetComponent<BoxCollider>().enabled = false;
    GetComponent<Rigidbody>().useGravity = false;

    //reset cell and recalculat path for player
    _changeCellTypeEvent.RaiseEvent((int)transform.position.x / GridConfig.CellSize.x, (int)transform.position.z / GridConfig.CellSize.z, CellType.Walkable);
    _changeNodeAccessibleEvent.RaiseEvent((int)transform.position.x / GridConfig.CellSize.x, (int)transform.position.z / GridConfig.CellSize.z, true);
    _recalculatePathEvent.RaiseEvent();


    //gain exp for player
    int expGain = _characterConfigSO.GetExpGain();
    _gainExpEvent?.RaiseEvent(expGain);

    //gain gold for player
    int goldGain = _characterConfigSO.GetGoldGain();
    _gainGoldEvent.RaiseEvent(goldGain);

    //remove enemy from queue
    _removeEnemyFromQueueEvent?.RaiseEvent(GetComponent<Enemy>());
    _onTurnCycleExecuted.RaiseEvent();
  }

  private void OnPlayerDeath()
  {
    _gameOverModalSetActiveEvent.RaiseEvent(true);
  }
}
