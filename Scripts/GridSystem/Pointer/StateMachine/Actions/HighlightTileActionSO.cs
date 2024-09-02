using UnityEngine;

[CreateAssetMenu(fileName = "HighlightTileAction", menuName = "StateMachine/Actions/Pointer/HighlightTileAction")]
public class HighlightTileActionSO : StateActionSO<HighlightTileAction> { }

public class HighlightTileAction : StateAction
{
  private Vector3Int _previousGridPosition;
  private Vector3Int _currentGridPosition;
  private PointerManager _pointerManager;
  //TODO: jadiin Grid menjadi variable global
  public override void Awake(StateMachine stateMachine)
  {
    _pointerManager = stateMachine.GetComponent<PointerManager>();
  }

  public override void OnStateEnter()
  {
    _previousGridPosition = _currentGridPosition = _pointerManager.GridPosition;
    HighlightTile(_currentGridPosition.x, _currentGridPosition.y);
  }

  public override void OnStateExit()
  {
    Unhighlight(_currentGridPosition.x, _currentGridPosition.y);
  }

  public override void OnUpdate()
  {
    _currentGridPosition = _pointerManager.GridPosition;

    //Cek kalau cursor pindah tile
    if (_currentGridPosition != _previousGridPosition)
    {
      Unhighlight(_previousGridPosition.x, _previousGridPosition.y);
      HighlightTile(_currentGridPosition.x, _currentGridPosition.y);
      _previousGridPosition = _currentGridPosition;
    }
  }

  private void HighlightTile(int x, int y)
  {
    //TODO: Dari pada getcomponent, mau ganti ke effect hover 2D aja biar ga berat di batches nya
    //TODO: Solusi, instantiate, lalu di destroy

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