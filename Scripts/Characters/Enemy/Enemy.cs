using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
  public UnityAction OnTurnExecuted;
  public PathStorageSO PathStorage;

  public bool IsReadyToChase = false;

  private void Awake()
  {
    PathStorage = ScriptableObject.CreateInstance<PathStorageSO>();
  }

  public void ExecuteTurn()
  {
    if (OnTurnExecuted != null)
      OnTurnExecuted.Invoke();
  }
}
