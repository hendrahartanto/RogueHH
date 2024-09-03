using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField] private GameStateSO _gameState = default;

  private void Start()
  {
    _gameState.SetGameState(GameState.Regular);
  }
}
