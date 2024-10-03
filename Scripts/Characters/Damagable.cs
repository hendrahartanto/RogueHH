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

  public CharacterConfigSO _characterConfigSO = default;

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
  [SerializeField] private BoolEventChannelSO _raycastSetActiveEvent = default;
  [SerializeField] private GameStateEventChanelSO _changeGameStateEvent = default;
  [SerializeField] private VoidEventChannelSO _removeAllTurnQueueEvent = default;
  public IntEventChanelSO SetMaxHealthUIEvent = default;
  public IntEventChanelSO UpdateHealthUIEvent = default;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _playerLevelUpEvent = default;

  private void Start()
  {
    if (_currentHealth == null)
    {
      _currentHealth = ScriptableObject.CreateInstance<HealthSO>();
    }

    SetupStats();

    _currentHealth.SetCurrentHealth(_currentHealth.MaxHealth);
    UpdateHealthUIEvent.RaiseEvent(_currentHealth.CurrentHealth);
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
    int maxHealth = _characterConfigSO.GetInitialHealth();

    _currentHealth.SetMaxHealth(maxHealth);

    //setup health ui
    SetMaxHealthUIEvent.RaiseEvent(_currentHealth.MaxHealth);
    UpdateHealthUIEvent.RaiseEvent(_currentHealth.CurrentHealth);

    DeffendPoint = _characterConfigSO.GetInitialDeffendPoint();
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
        OnEnemyDeath();

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
    _gainGoldEvent?.RaiseEvent(goldGain);

    //remove enemy from queue
    _removeEnemyFromQueueEvent?.RaiseEvent(GetComponent<Enemy>());
    _onTurnCycleExecuted.RaiseEvent();
  }

  private void OnPlayerDeath()
  {
    _changeGameStateEvent?.RaiseEvent(GameState.Gameover);
    _raycastSetActiveEvent?.RaiseEvent(false);
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
    else if (_source.gameObject.CompareTag("Enemy"))
    {
      IsDead = false;
      _gameOverModalSetActiveEvent?.RaiseEvent(true);
      _removeAllTurnQueueEvent?.RaiseEvent();
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
}
