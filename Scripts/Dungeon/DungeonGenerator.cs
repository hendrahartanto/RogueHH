using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
  [SerializeField] private GridTileSO _grid;
  [SerializeField] private GridNodeSO _gridNode;

  [SerializeField]
  public DungeonSO Dungeon;
  public List<RoomSO> PossibleRooms;

  private List<Room> _rooms;
  private List<Vector3Int> _roomCenters;

  private void Start()
  {
    Generate();
  }

  private void Generate()
  {
    _rooms = new List<Room>();
    _grid.Initialize(Dungeon.Size);
    _gridNode.Initialize(Dungeon.Size);
    _roomCenters = new List<Vector3Int>();

    PlaceRooms();
    ConnectRooms();
    PlaceDecorations();
  }

  private void PlaceRooms()
  {
    int roomCount = 0;
    while (roomCount < Dungeon.RoomCount)
    {
      Vector3Int location = new Vector3Int(
        GlobalRandom.Next(0, Dungeon.Size.x),
        0,
        GlobalRandom.Next(0, Dungeon.Size.y)
      );

      Vector3Int roomSize = new Vector3Int(
        GlobalRandom.Next(PossibleRooms[0].roomMinSize.x, PossibleRooms[0].roomMaxSize.x),
        0,
        GlobalRandom.Next(PossibleRooms[0].roomMinSize.y, PossibleRooms[0].roomMaxSize.y)
      );

      bool add = true;
      //TODO: possible roomsnya bikin random
      Room newRoom = new Room(location, roomSize, PossibleRooms[0]);
      Room buffer = new Room(location + new Vector3Int(-2, 0, -2), roomSize + new Vector3Int(4, 0, 4), PossibleRooms[0]);

      foreach (var room in _rooms)
      {
        if (Room.Intersect(room, buffer))
        {
          add = false;
          break;
        }
      }

      //TODO: experimental solution to out of bound grid access
      if (newRoom.area.xMin < 0 || newRoom.area.xMax + 1 >= Dungeon.Size.x - 1
          || newRoom.area.zMin < 0 || newRoom.area.zMax + 1 >= Dungeon.Size.y - 1)
      {
        add = false;
      }

      if (add)
      {
        _rooms.Add(newRoom);
        Dungeon.rooms.Add(newRoom);

        PlaceRoom(newRoom);
        roomCount++;
      }
    }
  }

  private void PlaceRoom(Room room)
  {
    PlaceTiles(room);
  }

  void PlaceTiles(Room room)
  {
    foreach (Vector3Int pos in room.area)
    {
      Tile newTile = new Tile(PossibleRooms[0].ChooseRandomFloor(), pos.x, pos.z, CellType.Walkable);
      _grid[pos.x, pos.z] = newTile;

      Node newNode = new Node(new Vector3Int(pos.x, 0, pos.z));
      _gridNode[pos.x, pos.z] = newNode;

      newTile.TileObject = Instantiate(newTile.tilePrefab, new Vector3(newTile.xWorld, 0, newTile.zWorld) + GridConfig.Offset, Quaternion.identity);
    }
  }

  void ConnectRooms()
  {
    foreach (var room in _rooms)
    {
      Vector3Int center = Vector3Int.RoundToInt(room.area.center);
      _roomCenters.Add(center);
    }

    //pilih point room center pertama starting point (dalam konteks ini diambil secara random)
    //tapi bisa direvisi untuk room yang tipenya spawn point
    var currentRoomCenter = _roomCenters[GlobalRandom.Next(0, _roomCenters.Count)];
    _roomCenters.Remove(currentRoomCenter);

    while (_roomCenters.Count > 0)
    {
      Vector3Int closest = FindClosestPoint(currentRoomCenter, _roomCenters);
      _roomCenters.Remove(closest);

      //function untuk membuat corridor dari currCenter ke clossest room center
      CreateCorridor(currentRoomCenter, closest);

      currentRoomCenter = closest;

    }
  }

  void CreateCorridor(Vector3Int position, Vector3Int destination)
  {
    Vector3Int xOffset = new Vector3Int(1, 0, 0);
    Vector3Int zOffset = new Vector3Int(0, 0, 1);

    bool startEntrance = true;
    bool endEntrance = true;

    int dirZ = 0;
    int dirX = 0;
    if (position.z != destination.z)
    {
      dirZ = (destination.z - position.z) / Math.Abs(destination.z - position.z);
    }
    if (position.x != destination.x)
    {
      dirX = (destination.x - position.x) / Math.Abs(destination.x - position.x);
    }

    while (position.z != destination.z)
    {
      //position z akan dimanipulasi agar menuju destinasi z
      position += zOffset * dirZ;
      if (_grid[position.x, position.z] == null)
      {
        if (startEntrance)
        {
          Vector3Int temp = position - zOffset * dirZ;
          Tile cell = _grid[position.x, temp.z];
          cell.cellTypes.Add(CellType.DecorationRestrict);
          startEntrance = false;
        }
        Tile newTile = new Tile(PossibleRooms[0].ChooseRandomFloor(), position.x, position.z, CellType.Walkable);
        _grid[position.x, position.z] = newTile;

        Node newNode = new Node(new Vector3Int(position.x, 0, position.z));
        _gridNode[position.x, position.z] = newNode;

        newTile.TileObject = Instantiate(newTile.tilePrefab, new Vector3(newTile.xWorld, 0, newTile.zWorld) + GridConfig.Offset, Quaternion.identity);
      }
    }
    while (position.x != destination.x)
    {
      //position x akan dimanipulasi agar menuju destinasi x
      position += xOffset * dirX;
      if (_grid[position.x, position.z] == null)
      {
        if (startEntrance)
        {
          Vector3Int temp = position - xOffset * dirX;
          Tile cell = _grid[temp.x, position.z];
          cell.cellTypes.Add(CellType.DecorationRestrict);
          startEntrance = false;
        }
        Tile newTile = new Tile(PossibleRooms[0].ChooseRandomFloor(), position.x, position.z, CellType.Walkable);
        _grid[position.x, position.z] = newTile;

        Node newNode = new Node(new Vector3Int(position.x, 0, position.z));
        _gridNode[position.x, position.z] = newNode;

        newTile.TileObject = Instantiate(newTile.tilePrefab, new Vector3(newTile.xWorld, 0, newTile.zWorld) + GridConfig.Offset, Quaternion.identity);
      }
      else
      {
        if (startEntrance == false && endEntrance)
        {
          Tile cell = _grid[position.x, position.z];
          cell.cellTypes.Add(CellType.DecorationRestrict);
          endEntrance = false;
        }
      }
    }
  }

  private Vector3Int FindClosestPoint(Vector3Int currentRoomCenter, List<Vector3Int> roomCenters)
  {
    Vector3Int closest = Vector3Int.zero;
    float distance = float.MaxValue;

    foreach (var position in roomCenters)
    {
      float currentDistance = Vector3.Distance(position, currentRoomCenter);
      if (currentDistance < distance)
      {
        distance = currentDistance;
        closest = position;
      }
    }
    return closest;
  }

  private void PlaceDecorations()
  {
    foreach (var room in _rooms)
    {
      //asumsi min decoration buffernya adalah 3
      while (room.availableTile >= 3)
      {
        DecorationSO decoration = room.roomType.ChooseRandomDecoration();

        int rotationAngle = GlobalRandom.Next(0, 4) * 90; // Randomly picks 0, 90, 180, or 270

        rotationAngle = 180;

        Vector3Int rotatedSize = decoration.size;
        int rotatedBufferX = decoration.bufferX;
        int rotatedBufferZ = decoration.bufferZ;
        float rotatedOffsetX = decoration.OffsetX;
        float rotatedOffsetZ = decoration.OffsetZ;

        int positionZOffset = 0;
        int positionXOffset = 0;

        switch (rotationAngle)
        {
          case 90:
            rotatedSize = new Vector3Int(decoration.size.z, decoration.size.y, decoration.size.x);
            rotatedBufferX = decoration.bufferZ;
            rotatedBufferZ = decoration.bufferX;

            float tempOffset90 = rotatedOffsetX;
            rotatedOffsetX = rotatedOffsetZ;
            rotatedOffsetZ = -tempOffset90;
            break;

          case 180:
            rotatedOffsetX = -decoration.OffsetX;
            rotatedOffsetZ = -decoration.OffsetZ;
            positionZOffset = decoration.size.z - 1;
            positionXOffset = decoration.size.x - 1;
            break;

          case 270:
            rotatedSize = new Vector3Int(decoration.size.z, decoration.size.y, decoration.size.x);
            rotatedBufferX = decoration.bufferZ;
            rotatedBufferZ = decoration.bufferX;

            float tempOffset270 = rotatedOffsetX;
            rotatedOffsetX = -rotatedOffsetZ;
            rotatedOffsetZ = tempOffset270;
            break;
        }

        Vector3Int position = new Vector3Int(
            GlobalRandom.Next(room.area.xMin, room.area.xMax),
            0,
            GlobalRandom.Next(room.area.zMin, room.area.zMax)
        );

        RectXZ decorationAreaBuffer = new RectXZ(position.x - rotatedBufferX - positionXOffset, position.z - rotatedBufferZ - positionZOffset, rotatedSize.x + rotatedBufferX * 2, rotatedSize.z + rotatedBufferZ * 2);
        RectXZ decorationArea = new RectXZ(position.x - positionXOffset, position.z - positionZOffset, rotatedSize.x, rotatedSize.z);

        bool add = true;

        if (decorationAreaBuffer.xMin < room.area.xMin || decorationAreaBuffer.xMax >= room.area.xMax
            || decorationAreaBuffer.zMin < room.area.zMin || decorationAreaBuffer.zMax >= room.area.zMax)
        {
          continue;
        }

        foreach (Vector3Int pos in decorationAreaBuffer)
        {
          if (_grid[pos.x, pos.z].cellTypes.Contains(CellType.DecorationRestrict) || _grid[pos.x, pos.z].cellTypes.Contains(CellType.Restricted))
          {
            add = false;
            break;
          }
        }

        if (add)
        {
          Quaternion rotation = Quaternion.Euler(0, rotationAngle, 0);

          Vector3 instantiationPosition = new Vector3(
              (position.x * GridConfig.CellSize.x) + rotatedOffsetX,
              decoration.Ypos,
              (position.z * GridConfig.CellSize.z) + rotatedOffsetZ
          ) + GridConfig.Offset;

          Instantiate(decoration.prefab, instantiationPosition, rotation);

          DecorationRestrict(decorationAreaBuffer);
          UnWalkableTile(decorationArea);
          room.availableTile -= decorationAreaBuffer.size.x * decorationAreaBuffer.size.z;

          Debug.Log("Available tile: " + room.availableTile);
        }
      }
    }
  }

  private void UnWalkableTile(RectXZ area)
  {
    foreach (Vector3Int pos in area)
    {
      _grid[pos.x, pos.z].cellTypes.Clear();
      _grid[pos.x, pos.z].cellTypes.Add(CellType.Restricted);
      _gridNode[pos.x, pos.z] = null;
    }
  }

  private void DecorationRestrict(RectXZ area)
  {
    foreach (Vector3Int pos in area)
    {
      _grid[pos.x, pos.z].cellTypes.Add(CellType.DecorationRestrict);
    }
  }
}
public class Room
{
  public RectXZ area;
  public RoomSO roomType;
  public int availableTile;

  public int EnemyCount;

  public Room(Vector3Int location, Vector3Int size, RoomSO roomType)
  {
    area = new RectXZ(location.x, location.z, size.x, size.z);

    //berapa banyak slot tile untuk diletakkan suatu dekorasi
    availableTile = GlobalRandom.Next(Mathf.RoundToInt(0.2f * size.x * size.z), Mathf.RoundToInt(0.3f * size.x * size.z)) - 2;

    Debug.Log(availableTile);

    this.roomType = roomType;

    EnemyCount = 0;
  }

  public static bool Intersect(Room a, Room b)
  {
    return !((a.area.position.x >= (b.area.position.x + b.area.size.x)) || ((a.area.position.x + a.area.size.x) <= b.area.position.x)
        || (a.area.position.z >= (b.area.position.z + b.area.size.z)) || ((a.area.position.z + a.area.size.z) <= b.area.position.z));
  }
}

public class Tile
{
  public GameObject tilePrefab;
  public GameObject TileObject;
  public int x;
  public int z;
  public List<CellType> cellTypes = new List<CellType>();

  public Tile(GameObject tilePrefab, int x, int z, CellType cellType)
  {
    this.tilePrefab = tilePrefab;
    this.x = x;
    this.z = z;
    cellTypes.Add(cellType);
  }

  public int xWorld { get { return x * GridConfig.CellSize.x; } }
  public int zWorld { get { return z * GridConfig.CellSize.z; } }
}

public enum CellType
{
  Walkable,
  DecorationRestrict,
  Restricted,
  Enemy
}
