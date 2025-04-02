using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
  Regular,
  Alert,
  Combat,
  Gameover,
}

[CreateAssetMenu(fileName = "GameState", menuName = "Gameplay/GameState")]
public class GameStateSO : ScriptableObject
{
  public GameState CurrentGameState;
  public bool IsTurnCycling = false;

  public void SetGameState(GameState newGameState)
  {
    CurrentGameState = newGameState;
  }

  public void SetIsTurnCycling(bool isTurnCycling)
  {
    IsTurnCycling = isTurnCycling;
  }
}
