using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IState
{
  void OnStateEnter();
  void OnStateExit();
}
