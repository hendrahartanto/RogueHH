using UnityEngine;
public class PlayerSpawnSystem : MonoBehaviour
{
  //TODO: REMOVE LATER
  [SerializeField] private GameObject _playerPrefab;

  [SerializeField] private GridTileSO _grid;
  [SerializeField] private DungeonSO _dungeon;
  [SerializeField] private TransformAnchorSO _playerTransformAnchor = default;
  [SerializeField] private VoidEventChannelSO _onPlayerInstantiated = default;


  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _onDungeonGenerated = default;

  private void OnEnable()
  {
    _onDungeonGenerated.OnEventRaised += SpawnPlayer;
  }

  private void OnDisable()
  {
    _onDungeonGenerated.OnEventRaised -= SpawnPlayer;
    _playerTransformAnchor.Unset();
  }

  private void SpawnPlayer()
  {
    Vector2Int randomPosition;
    while (true)
    {
      randomPosition = new Vector2Int(
        GlobalRandom.Next(0, _dungeon.Size.x),
        GlobalRandom.Next(0, _dungeon.Size.y)
      );

      if (_grid[randomPosition.x, randomPosition.y] != null && _grid[randomPosition.x, randomPosition.y].cellTypes.Contains(CellType.Walkable))
        break;
    }

    //TODO: instansi masih dummy dan transform juga
    Transform spawnLocation = _playerPrefab.transform;

    spawnLocation.position = new Vector3(randomPosition.x * GridConfig.CellSize.x, 1.5f, randomPosition.y * GridConfig.CellSize.z) + GridConfig.Offset;
    spawnLocation.rotation = Quaternion.identity;

    GameObject playerInstance = Instantiate(_playerPrefab, spawnLocation.position, spawnLocation.rotation);

    _playerTransformAnchor.Provide(playerInstance.transform);
    _onPlayerInstantiated.RaiseEvent();
  }
}
