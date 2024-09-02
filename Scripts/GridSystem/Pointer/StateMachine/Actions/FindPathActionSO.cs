using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindPathAction", menuName = "StateMachine/Actions/Pointer/FindPathAction")]
public class FindPathActionSO : StateActionSO
{
  public TransformAnchorSO PlayerTransform;

  protected override StateAction CreateAction() => new FindPathAction();
}

public class FindPathAction : StateAction
{
  //TODO: Ganti cara highlight dari pada emission instantiate 2d bulet" biar hemat performance
  private AStar _aStar;
  private Player _player;
  private PathStorageSO _pathStorage;
  private TransformAnchorSO _playerTransform; //untuk posisi player sekarang / startNode
  private PointerManager _pointerManager;
  private GridNodeSO _gridNode;
  private Vector3Int _previousGridPosition;
  private Vector3Int _currentGridPosition;

  public override void Awake(StateMachine stateMachine)
  {
    Debug.Log("Awake Action");
    _pointerManager = stateMachine.GetComponent<PointerManager>();
    _player = stateMachine.GetComponent<Player>();
    _pathStorage = _player.PathStorage;
    _playerTransform = ((FindPathActionSO)OriginSO).PlayerTransform;
    _gridNode = _pointerManager.GridNode;
    _aStar = new AStar();
  }

  public override void OnStateEnter()
  {
    _previousGridPosition = _currentGridPosition = _pointerManager.GridPosition;
    Node startNode = _gridNode[(int)_playerTransform.Value.position.x / GridConfig.CellSize.x, (int)_playerTransform.Value.position.z / GridConfig.CellSize.z];
    Node endNode = _gridNode[_currentGridPosition.x, _currentGridPosition.y];

    _aStar.FindPath(_gridNode, _pathStorage, startNode, endNode);

    foreach (var path in _pathStorage.paths)
    {
      HighlightTile(path.Position.x, path.Position.z);
    }
  }

  public override void OnStateExit()
  {
    //unhighlight path sebelumnya
    foreach (var path in _pathStorage.paths)
    {
      Unhighlight(path.Position.x, path.Position.z);
    }

    //hapus isi pathStorage
    _pathStorage.paths.Clear();
  }

  public override void OnUpdate()
  {
    _currentGridPosition = _pointerManager.GridPosition;

    //Cek kalau cursor pindah tile
    if (_currentGridPosition != _previousGridPosition)
    {
      //unhighlight path sebelumnya
      foreach (var path in _pathStorage.paths)
      {
        Unhighlight(path.Position.x, path.Position.z);
      }

      //hapus isi pathStorage
      _pathStorage.paths.Clear();

      Node startNode = _gridNode[(int)_playerTransform.Value.position.x / GridConfig.CellSize.x, (int)_playerTransform.Value.position.z / GridConfig.CellSize.z];
      Node endNode = _gridNode[_currentGridPosition.x, _currentGridPosition.y];

      _aStar.FindPath(_gridNode, _pathStorage, startNode, endNode);

      foreach (var path in _pathStorage.paths)
      {
        HighlightTile(path.Position.x, path.Position.z);
      }

      _previousGridPosition = _currentGridPosition;
    }
  }

  private void HighlightTile(int x, int y)
  {
    GameObject tileObject = _pointerManager.Grid[x, y].TileObject;
    Renderer renderer = tileObject.GetComponentInChildren<Renderer>();

    Material material = renderer.material;
    material.EnableKeyword("_EMISSION");

    Color emissionColor = Color.white * 0.2f;
    material.SetColor("_EmissionColor", emissionColor);
  }

  private void Unhighlight(int x, int y)
  {
    GameObject tileObject = _pointerManager.Grid[x, y].TileObject;

    Renderer renderer = tileObject.GetComponentInChildren<Renderer>();

    Material material = renderer.material;
    material.DisableKeyword("_EMISSION");
    material.SetColor("_EmissionColor", Color.black);
  }
}
