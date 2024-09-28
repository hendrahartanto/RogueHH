using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
  Regular,
  Combat,
  TurnCycling,
  Gameover,
}

[CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState")]
public class GameStateSO : ScriptableObject
{
  public GameState CurrentGameState;

  public void SetGameState(GameState newGameState)
  {
    CurrentGameState = newGameState;
  }
}
