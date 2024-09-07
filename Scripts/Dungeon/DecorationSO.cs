using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decoration", menuName = "Dungeon/Decoration")]
public class DecorationSO : ScriptableObject
{
  public GameObject prefab;
  public Vector3Int size;
  public float Ypos;
  public float OffsetX;
  public float OffsetZ;
  public int bufferX;
  public int bufferZ;
}
