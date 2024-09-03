using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
  public List<ITurnComponent> QueueItems = default;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _onTurnCycleExecuted = default;
  [SerializeField] private TurnComponentEventChanelSO _onEnemyAggro = default;

  private void OnEnable()
  {
    _onTurnCycleExecuted.OnEventRaised += ExecuteTurnCycle;
    _onEnemyAggro.OnEventRaised += AddToQueue;
  }

  private void OnDisable()
  {
    _onTurnCycleExecuted.OnEventRaised -= ExecuteTurnCycle;
    _onEnemyAggro.OnEventRaised -= AddToQueue;
  }

  private void Awake()
  {
    QueueItems = new List<ITurnComponent>();
  }

  private void AddToQueue(ITurnComponent turnComponent)
  {
    Debug.Log("ADD TO TURN QUEUE");
    QueueItems.Add(turnComponent);
  }

  private void ExecuteTurnCycle()
  {
    Debug.Log("EXECUTE TURN CYCLE");
    QueueItems[0].ExecuteTurn();
  }

}
