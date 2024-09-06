using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
  public List<ITurnComponent> QueueItems = default;
  private int _currentItemIndex = 0;

  [Header("Broadcasting to")]
  [SerializeField] private GameStateEventChanelSO _turnCycleEvent = default;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _onTurnCycleExecuted = default;
  [SerializeField] private VoidEventChannelSO _onTurnFinished = default;
  [SerializeField] private TurnComponentEventChanelSO _onEnemyAggro = default;
  [SerializeField] private TurnComponentEventChanelSO _removeEnemyFromQueueEvent = default;


  private void OnEnable()
  {
    _onTurnCycleExecuted.OnEventRaised += ExecuteTurnCycle;
    _onEnemyAggro.OnEventRaised += AddToQueue;
    _onTurnFinished.OnEventRaised += ExecuteNextTurn;
    _removeEnemyFromQueueEvent.OnEventRaised += RemoveFromQueue;
  }

  private void OnDisable()
  {
    _onTurnCycleExecuted.OnEventRaised -= ExecuteTurnCycle;
    _onEnemyAggro.OnEventRaised -= AddToQueue;
    _onTurnFinished.OnEventRaised -= ExecuteNextTurn;
    _removeEnemyFromQueueEvent.OnEventRaised -= RemoveFromQueue;
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
  }

  private void ExecuteTurnCycle()
  {
    if (QueueItems.Count == 0)
      return;

    _turnCycleEvent.RaiseEvent(GameState.TurnCycling);

    _currentItemIndex = 0;
    QueueItems[_currentItemIndex++].ExecuteTurn();
  }

  private void ExecuteNextTurn()
  {
    if (_currentItemIndex >= QueueItems.Count)
    {
      _turnCycleEvent.RaiseEvent(GameState.Combat);
      return;
    }

    QueueItems[_currentItemIndex++].ExecuteTurn();
  }

}
