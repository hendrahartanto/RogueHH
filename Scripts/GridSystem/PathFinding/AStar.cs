using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
  public void FindPath(GridNodeSO gridSource, PathStorageSO pathStorage, Node startNode, Node endNode)
  {
    //TODO: experimental membuat instance GridNodeSO baru agar valuenya ga tertimpa saat banyak entitiy menggunakan pathfinding
    GridNodeSO grid = ScriptableObject.CreateInstance<GridNodeSO>();
    grid.Initialize(gridSource.size, gridSource.data);

    int[] dirX = { 1, 0, -1, 0 };
    int[] dirZ = { 0, -1, 0, 1 };

    List<Node> openNodes = new List<Node>();
    List<Node> visitedNodes = new List<Node>();

    openNodes.Add(startNode);

    while (true)
    {
      //cari node dengan F cost terendah kalau ada lebih dari 1, cari berdasarkan HCost terendah
      Node curr = openNodes
        .OrderBy(node => node.FCost)
        .ThenBy(node => node.HCost)
        .FirstOrDefault();

      openNodes.Remove(curr);
      visitedNodes.Add(curr);

      //jika sudah mencapai endNode atau target
      if (curr == endNode)
      {
        pathStorage.paths = Backtrack(endNode, startNode);
        return;
      }

      //loop ke 4 neighbour (kiri, atas, bawah, kanan)
      for (int i = 0; i < 4; i++)
      {
        //cek jika out of boud
        if (curr.Position.x + dirX[i] > grid.size.x || curr.Position.z + dirX[i] > grid.size.y)
          continue;

        Node currNeighbour = grid[curr.Position.x + dirX[i], curr.Position.z + dirZ[i]];

        //Cek node neighbor sekarang kalau null atau udah divisit maka di skip ke neighbour selanjutnya
        if (currNeighbour == null || visitedNodes.Contains(currNeighbour))
          continue;

        //hitung costnya menggunakan eucledian distance di c# udah ada function bawaan Vector3int yaitu Distance
        CalculateCost(out float Gcost, out float HCost, out float FCost, startNode, currNeighbour, endNode);

        //jika cost currNeighbour lebih pendek dibanding evaluasi sebelumnya atau currNeighbour belum ada di openlist
        if (FCost < currNeighbour.FCost || !openNodes.Contains(currNeighbour))
        {
          currNeighbour.GCost = Gcost;
          currNeighbour.HCost = HCost;
          currNeighbour.FCost = FCost;

          currNeighbour.Parent = curr;

          if (!openNodes.Contains(currNeighbour))
            openNodes.Add(currNeighbour);
        }
      }
    }
  }

  private void CalculateCost(out float GCost, out float HCost, out float FCost, Node startNode, Node currNeighbour, Node endNode)
  {
    GCost = Vector3Int.Distance(currNeighbour.Position, startNode.Position);
    HCost = Vector3Int.Distance(currNeighbour.Position, endNode.Position);
    FCost = GCost + HCost;
  }

  public List<Node> Backtrack(Node endNode, Node startNode)
  {
    List<Node> path = new List<Node>();
    Node currentNode = endNode;

    while (currentNode != startNode)
    {
      path.Add(currentNode);
      currentNode = currentNode.Parent;
    }

    path.Reverse();
    return path;
  }

}

public class Node
{
  public Vector3Int Position;
  public Node Parent;
  public float GCost, HCost, FCost;
  //GCost: jarak start ke curr, HCost: jarak end ke curr, FCost: GCost + HCost (biasanya FCost yang digunakan untuk evaluasi) 
  public Vector3Int WorldPosition { get { return new Vector3Int(Position.x * GridConfig.CellSize.x, 0, Position.z * GridConfig.CellSize.z); } }

  public Node(Vector3Int position)
  {
    Position = position;
    Parent = null;
    GCost = HCost = FCost = 0;
  }
}
