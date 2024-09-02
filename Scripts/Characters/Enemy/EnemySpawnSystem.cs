using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
  [SerializeField] DungeonSO _dungeon;
  [SerializeField] private GridTileSO _grid;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _onSceneReady = default;
  private void OnEnable()
  {
    _onSceneReady.OnEventRaised += SpawnEnemy;
  }

  private void OnDisable()
  {
    _onSceneReady.OnEventRaised -= SpawnEnemy;
  }

  private void SpawnEnemy()
  {
    //TODO: this is experimental config
    int enemyCount = _dungeon.RoomCount / 2;
    int currEnemyCount = 0;
    int maxEnemyPerRoom = 1;

    while (currEnemyCount < enemyCount)
    {
      Vector2Int randomPosition;
      Room randomRoom = _dungeon.rooms[GlobalRandom.Next(0, _dungeon.RoomCount)];

      if (randomRoom.EnemyCount >= maxEnemyPerRoom)
        continue;

      while (true)
      {
        randomPosition = new Vector2Int(
          GlobalRandom.Next(randomRoom.area.xMin, randomRoom.area.xMax),
          GlobalRandom.Next(randomRoom.area.zMin, randomRoom.area.zMax)
        );

        if (_grid[randomPosition.x, randomPosition.y] != null && _grid[randomPosition.x, randomPosition.y].cellTypes.Contains(CellType.Walkable))
          break;
      }

      EnemyBaseSO enemy = _dungeon.GetRandomEnemy();

      Transform spawnLocation = enemy.Prefab.transform;

      spawnLocation.position = new Vector3(randomPosition.x * GridConfig.CellSize.x, 1.5f, randomPosition.y * GridConfig.CellSize.z) + GridConfig.Offset;
      spawnLocation.rotation = Quaternion.identity;

      Instantiate(enemy.Prefab, spawnLocation.position, spawnLocation.rotation);

      randomRoom.EnemyCount++;
      currEnemyCount++;
    }
  }
}
