using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridNode", menuName = "GridSystem/GridNode")]
public class GridNodeSO : GridBase<Node>
{
  public void Initialize(Vector2Int size, Node[] data)
  {
    this.size = size;
    this.data = (Node[])data.Clone();
  }
}
