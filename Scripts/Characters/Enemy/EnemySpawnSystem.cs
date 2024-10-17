using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnSystem : MonoBehaviour
{
  [SerializeField] DungeonSO _dungeon;
  [SerializeField] private GridTileSO _grid;

  [Header("Configs")]
  private int _baseEnemyCount;
  private int _actualEnemyCount;
  public int MaxEnemyCount;
  public int MaxEnemiesPerRoomLimit;
  public int MaxLevel;
  public List<EnemyBaseSO> PossibleEnemyTypes = new List<EnemyBaseSO>();
  public List<int> EnemyTypeChances;
  public List<int> BaseChances;
  public List<int> FinalChances;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _onDungeonGenerated = default;

  [Header("Broadcasting to")]
  [SerializeField] private ChangeCellTypeEventChanel _changeCellTypeEvent = default;
  [SerializeField] private GridNodeBoolEventChanelSO _changeNodeAccessibleEvent = default;
  [SerializeField] private IntEventChanelSO _updateEnemyIndicatorUIEvent = default;

  private void Awake()
  {
    _baseEnemyCount = (int)(_dungeon.RoomCount / 1.5);
  }

  private void OnEnable()
  {
    _onDungeonGenerated.OnEventRaised += SpawnEnemy;
  }

  private void OnDisable()
  {
    _onDungeonGenerated.OnEventRaised -= SpawnEnemy;
  }

  private int ScaleEnemyCount(int level)
  {
    return (int)Mathf.Clamp(Mathf.Lerp(_baseEnemyCount, MaxEnemyCount, (float)level / MaxLevel), _baseEnemyCount, MaxEnemyCount);
  }

  public EnemyBaseSO GetRandomEnemyType()
  {
    int totalChance = 0;
    foreach (int chance in EnemyTypeChances)
    {
      totalChance += chance;
    }

    int randomValue = Random.Range(0, totalChance);
    int cumulativeChance = 0;

    for (int i = 0; i < PossibleEnemyTypes.Count; i++)
    {
      cumulativeChance += EnemyTypeChances[i];
      if (randomValue < cumulativeChance)
      {
        return PossibleEnemyTypes[i];
      }
    }

    return null;
  }

  private void SetupSpawnChances()
  {
    int currentLevel = _dungeon.CurrentLevel;

    EnemyTypeChances[0] = (int)Mathf.Lerp(BaseChances[0], FinalChances[0], (float)(currentLevel - 1) / (MaxLevel - 1));
    EnemyTypeChances[1] = (int)Mathf.Lerp(BaseChances[1], FinalChances[1], (float)(currentLevel - 1) / (MaxLevel - 1));
    EnemyTypeChances[2] = (int)Mathf.Lerp(BaseChances[2], FinalChances[2], (float)(currentLevel - 1) / (MaxLevel - 1));
  }

  private void SpawnEnemyPerType(int enemyCount, int type, int maxEnemyPerRoom)
  {
    int currEnemyCount = 0;
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

        if (_grid[randomPosition.x, randomPosition.y] != null && _grid[randomPosition.x, randomPosition.y].cellTypes.Contains(CellType.Walkable) && !_grid[randomPosition.x, randomPosition.y].cellTypes.Contains(CellType.PlayerBuffer))
          break;
      }

      EnemyBaseSO enemy = PossibleEnemyTypes[type];

      _changeCellTypeEvent.RaiseEvent(randomPosition.x, randomPosition.y, CellType.Enemy);
      _changeNodeAccessibleEvent.RaiseEvent(randomPosition.x, randomPosition.y, false);

      Transform spawnLocation = enemy.GetRandomPrefab().transform;

      spawnLocation.position = new Vector3(randomPosition.x * GridConfig.CellSize.x, 1.5f, randomPosition.y * GridConfig.CellSize.z) + GridConfig.Offset;
      spawnLocation.rotation = Quaternion.identity;

      GameObject enemyObject = spawnLocation.gameObject;

      SetEnemeyLabel(enemyObject, type);

      //assign unique channel to each enemy
      IntEventChanelSO setMaxhealthEvent = ScriptableObject.CreateInstance<IntEventChanelSO>();
      IntEventChanelSO updateHealthUIEvent = ScriptableObject.CreateInstance<IntEventChanelSO>();
      VoidEventChannelSO toggleAlertIndicatorEvent = ScriptableObject.CreateInstance<VoidEventChannelSO>();

      UIBarManager UIHealthBarManagerComp = enemyObject.GetComponentInChildren<UIBarManager>();
      UIHealthBarManagerComp.SetMaxHealthUIEvent = setMaxhealthEvent;
      UIHealthBarManagerComp.UpdateHealthUIEvent = updateHealthUIEvent;

      Damagable DamagableComp = enemyObject.GetComponent<Damagable>();
      DamagableComp.SetMaxHealthUIEvent = setMaxhealthEvent;
      DamagableComp.UpdateHealthUIEvent = updateHealthUIEvent;

      Enemy enemyComp = enemyObject.GetComponent<Enemy>();
      enemyComp.ToggleAlertIndicatorEvent = toggleAlertIndicatorEvent;

      EnemyAggroTrigger enemyAggroTriggerComp = enemyObject.GetComponentInChildren<EnemyAggroTrigger>();
      enemyAggroTriggerComp.ToggleAlertIndicatorEvent = toggleAlertIndicatorEvent;

      //set the level of the enemy
      DamagableComp._characterConfigSO.Level = _dungeon.CurrentLevel;

      //instantiate object yang udah dikasih chanel unique
      Instantiate(enemyObject, spawnLocation.position, spawnLocation.rotation);

      randomRoom.EnemyCount++;
      currEnemyCount++;
      _actualEnemyCount++;
    }
  }

  private void SetEnemeyLabel(GameObject enemy, int type)
  {
    Enemy enemyComp = enemy.GetComponent<Enemy>();
    enemyComp.EnemyLabel.SetText(RandomInitial.GetRandomInitial());
    enemyComp.EnemyLabel.color = RandomInitial.GetRarityColor(type);
  }

  private void SpawnEnemy()
  {
    _actualEnemyCount = 0;

    SetupSpawnChances();

    int enemyCount = ScaleEnemyCount(_dungeon.CurrentLevel);

    int easyCount = enemyCount * EnemyTypeChances[0] / 100;
    int mediumCount = enemyCount * EnemyTypeChances[1] / 100;
    int hardCount = enemyCount * EnemyTypeChances[2] / 100;

    int maxEnemyPerRoom = enemyCount / _dungeon.RoomCount + 1;

    SpawnEnemyPerType(easyCount, 0, maxEnemyPerRoom);
    SpawnEnemyPerType(mediumCount, 1, maxEnemyPerRoom);
    SpawnEnemyPerType(hardCount, 2, maxEnemyPerRoom);

    //set enemy ui indicator
    _updateEnemyIndicatorUIEvent.RaiseEvent(_actualEnemyCount);
  }
}
