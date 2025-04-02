using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase<T> : ScriptableObject
{
  public T[] data;
  public Vector2Int size;
  public void Initialize(Vector2Int size)
  {
    this.size = size;
    data = new T[size.x * size.y];
  }

  public int GetIndex(Vector2Int pos)
  {
    return pos.x + (size.x * pos.y);
  }

  public bool InBounds(Vector2Int pos)
  {
    return new RectInt(Vector2Int.zero, size).Contains(pos);
  }

  public T this[int x, int y]
  {
    get
    {
      return this[new Vector2Int(x, y)];
    }
    set
    {
      this[new Vector2Int(x, y)] = value;
    }
  }

  public T this[Vector2Int pos]
  {
    get
    {
      return data[GetIndex(pos)];
    }
    set
    {
      data[GetIndex(pos)] = value;
    }
  }
}