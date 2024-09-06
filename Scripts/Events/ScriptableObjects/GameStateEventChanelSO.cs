using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/GameStateEventChanel")]
public class GameStateEventChanelSO : ScriptableObject
{
  public UnityAction<GameState> OnEventRaised;

  public void RaiseEvent(GameState gameState)
  {
    if (OnEventRaised != null)
      OnEventRaised.Invoke(gameState);
  }
}
