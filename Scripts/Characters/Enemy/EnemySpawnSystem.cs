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
    int enemyCount = _dungeon.RoomCount * 2; // bagi 2 
    int currEnemyCount = 0;
    int maxEnemyPerRoom = 2;

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

      EnemyBaseSO enemy = _dungeon.GetRandomEnemyType();

      //set tile to type to enemy
      _grid[randomPosition.x, randomPosition.y].cellTypes.Clear();
      _grid[randomPosition.x, randomPosition.y].cellTypes.Add(CellType.Enemy);

      Transform spawnLocation = enemy.GetRandomPrefab().transform;

      spawnLocation.position = new Vector3(randomPosition.x * GridConfig.CellSize.x, 1.5f, randomPosition.y * GridConfig.CellSize.z) + GridConfig.Offset;
      spawnLocation.rotation = Quaternion.identity;

      GameObject enemyObject = spawnLocation.gameObject;

      //assign unique helathevent chanel to each enemy
      IntEventChanelSO setMaxhealthEvent = ScriptableObject.CreateInstance<IntEventChanelSO>();
      IntEventChanelSO updateHealthUIEvent = ScriptableObject.CreateInstance<IntEventChanelSO>();

      UIHealthBarManager UIHealthBarManagerComp = enemyObject.GetComponentInChildren<UIHealthBarManager>();
      UIHealthBarManagerComp.SetMaxHealthUIEvent = setMaxhealthEvent;
      UIHealthBarManagerComp.UpdateHealthUIEvent = updateHealthUIEvent;

      Damagable DamagableComp = enemyObject.GetComponent<Damagable>();
      DamagableComp.SetMaxHealthUIEvent = setMaxhealthEvent;
      DamagableComp.UpdateHealthUIEvent = updateHealthUIEvent;

      //instantiate object yang udah dikasih chanel unique
      Instantiate(enemyObject, spawnLocation.position, spawnLocation.rotation);

      randomRoom.EnemyCount++;
      currEnemyCount++;
    }
  }
}
