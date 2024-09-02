using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RectXZ : IEnumerable<Vector3Int>
{
  public int x;
  public int z;
  public int width; //x
  public int depth; //z

  public RectXZ(int x, int z, int width, int depth)
  {
    this.x = x;
    this.z = z;
    this.width = width;
    this.depth = depth;
  }
  public int xWorld { get { return x * 2; } }
  public int zWorld { get { return z * 2; } }
  public int widthWorld { get { return width * 2; } }
  public int depthWorld { get { return depth * 2; } }

  public int xMin { get { return x; } }
  public int xMax { get { return x + width; } }
  public int zMin { get { return z; } }
  public int zMax { get { return z + depth; } }

  public int xMinWorld { get { return xWorld; } }
  public int xMaxWorld { get { return xWorld + widthWorld; } }
  public int zMinWorld { get { return zWorld; } }
  public int zMaxWorld { get { return zWorld + depth * 2; } }

  public Vector3Int position { get { return new Vector3Int(x, 0, z); } }
  public Vector3Int size { get { return new Vector3Int(width, 0, depth); } }
  public Vector3 center => new Vector3(position.x + size.x / 2, 0, position.z + size.z / 2);

  public Vector3Int positionWorld { get { return new Vector3Int(x * 2, 0, zWorld); } }
  public Vector3Int sizeWorld { get { return new Vector3Int(widthWorld, 0, depth * 2); } }
  public Vector3 centerWorld
  {
    get
    {
      float x = positionWorld.x + size.x;
      float z = positionWorld.z + size.z;
      if ((int)x % 2 == 0)
      {
        x += 1;
      }

      if ((int)z % 2 == 0)
      {
        z += 1;
      }
      return new Vector3(x, 0, z);
    }
  }

  public bool Contains(Vector3Int point)
  {
    return point.x >= xMin && point.x < xMax && point.z >= zMin && point.z < zMax;
  }

  public bool Overlaps(RectXZ other)
  {
    return other.xMax > xMin && other.xMin < xMax && other.zMax > zMin && other.zMin < zMax;
  }

  public IEnumerator<Vector3Int> GetEnumerator()
  {
    for (int x = xMin; x < xMax; x++)
    {
      for (int z = zMin; z < zMax; z++)
      {
        yield return new Vector3Int(x, 0, z);
      }
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

}
