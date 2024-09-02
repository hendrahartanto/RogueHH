using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path Storage", menuName = "GridSystem/Path Storage")]
public class PathStorageSO : ScriptableObject
{
  public List<Node> paths;
}
