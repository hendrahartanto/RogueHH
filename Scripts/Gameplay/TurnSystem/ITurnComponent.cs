using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ITurnComponent
{
  UnityAction OnTurnExecuted { get; set; }
  void ExecuteTurn();
}
