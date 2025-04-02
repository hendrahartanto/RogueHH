using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PointerState", menuName = "GridSystem/PointerState")]
public class PointerStateSO : ScriptableObject
{
  public PointerState state;
}

public enum PointerState
{
  Restricted,
  Enemy,
  Path
}
