using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
  Regular,
  Combat,
  TurnCycling
}

public class GameStateSO : ScriptableObject
{
  public GameState CurrentGameState;

}
