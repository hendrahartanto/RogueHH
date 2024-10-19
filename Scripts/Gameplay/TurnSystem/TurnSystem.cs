using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
  public List<ITurnComponent> QueueItems = default;
  private int _currentItemIndex = 0;
  [SerializeField] private InputReader _inputReader = default;
  [SerializeField] private GameStateSO _gameState = default;

  [Header("Broadcasting to")]
  [SerializeField] private GameStateEventChanelSO _changeGameStateEvent = default;
  [SerializeField] private BoolEventChannelSO _isTurnCyclingSetActiveEvent = default;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _onTurnCycleExecuted = default;
  [SerializeField] private VoidEventChannelSO _onTurnFinished = default;
  [SerializeField] private TurnComponentEventChanelSO _onEnemyAggro = default;
  [SerializeField] private TurnComponentEventChanelSO _removeEnemyFromQueueEvent = default;
  [SerializeField] private VoidEventChannelSO _removeAllTurnQueueEvent = default;

  private void OnEnable()
  {
    _onTurnCycleExecuted.OnEventRaised += ExecuteTurnCycle;
    _onEnemyAggro.OnEventRaised += AddToQueue;
    _onTurnFinished.OnEventRaised += ExecuteNextTurn;
    _removeEnemyFromQueueEvent.OnEventRaised += RemoveFromQueue;
    _removeAllTurnQueueEvent.OnEventRaised += RemoveAllQueue;
    _inputReader.KeyboardSpaceEvent += _onTurnCycleExecuted.RaiseEvent;
  }

  private void OnDisable()
  {
    _onTurnCycleExecuted.OnEventRaised -= ExecuteTurnCycle;
    _onEnemyAggro.OnEventRaised -= AddToQueue;
    _onTurnFinished.OnEventRaised -= ExecuteNextTurn;
    _removeEnemyFromQueueEvent.OnEventRaised -= RemoveFromQueue;
    _removeAllTurnQueueEvent.OnEventRaised -= RemoveAllQueue;
    _inputReader.KeyboardSpaceEvent -= _onTurnCycleExecuted.RaiseEvent;
  }

  private void Awake()
  {
    QueueItems = new List<ITurnComponent>();
  }

  private void AddToQueue(ITurnComponent turnComponent)
  {
    if (QueueItems.Contains(turnComponent))
      return;

    QueueItems.Add(turnComponent);
  }

  private void RemoveFromQueue(ITurnComponent turnComponent)
  {
    QueueItems.Remove(turnComponent);

    if (QueueItems.Count <= 0)
      _changeGameStateEvent.RaiseEvent(GameState.Regular);
  }

  private void ExecuteTurnCycle()
  {
    if (QueueItems.Count <= 0)
    {
      _isTurnCyclingSetActiveEvent.RaiseEvent(false);
      return;
    }

    _isTurnCyclingSetActiveEvent.RaiseEvent(true);


    _currentItemIndex = 0;
    QueueItems[_currentItemIndex++].ExecuteTurn();
  }

  private void ExecuteNextTurn()
  {
    //jika udah diakhir queue (selesai semua) balikin gamestate ke sebelumnya
    if (_currentItemIndex >= QueueItems.Count)
    {
      _isTurnCyclingSetActiveEvent.RaiseEvent(false);
      return;
    }

    QueueItems[_currentItemIndex++].ExecuteTurn();
  }

  private void RemoveAllQueue()
  {
    QueueItems.Clear();
  }

}
