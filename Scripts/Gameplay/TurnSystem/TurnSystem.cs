using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
  public List<ITurnComponent> QueueItems = default;
  private int _currentItemIndex = 0;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _onTurnCycleExecuted = default;
  [SerializeField] private VoidEventChannelSO _onTurnFinished = default;
  [SerializeField] private TurnComponentEventChanelSO _onEnemyAggro = default;


  private void OnEnable()
  {
    _onTurnCycleExecuted.OnEventRaised += ExecuteTurnCycle;
    _onEnemyAggro.OnEventRaised += AddToQueue;
    _onTurnFinished.OnEventRaised += ExecuteNextTurn;
  }

  private void OnDisable()
  {
    _onTurnCycleExecuted.OnEventRaised -= ExecuteTurnCycle;
    _onEnemyAggro.OnEventRaised -= AddToQueue;
    _onTurnFinished.OnEventRaised -= ExecuteNextTurn;
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

  private void ExecuteTurnCycle()
  {
    _currentItemIndex = 0;
    QueueItems[_currentItemIndex++].ExecuteTurn();
  }

  private void ExecuteNextTurn()
  {
    if (_currentItemIndex >= QueueItems.Count)
      return;

    QueueItems[_currentItemIndex++].ExecuteTurn();
  }

}
