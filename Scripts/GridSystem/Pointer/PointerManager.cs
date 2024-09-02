using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerManager : MonoBehaviour
{
  public GridTileSO Grid = default;
  public GridNodeSO GridNode = default;
  public Vector3Int GridPosition = default;
  public bool isPointingNull = true;
  [SerializeField] private PointerStateSO _pointerState;
  [SerializeField] private Camera _mainCamera;
  [SerializeField] private VoidEventChannelSO _onPlayerInstantiated = default;

  private void SetupCamera()
  {
    GameObject mainCamera = GameObject.Find("Main Camera");
    _mainCamera = mainCamera.GetComponent<Camera>();
  }

  private void OnEnable()
  {
    _onPlayerInstantiated.OnEventRaised += SetupCamera;
  }

  private void OnDisable()
  {
    _onPlayerInstantiated.OnEventRaised -= SetupCamera;
  }

  private void Update()
  {
    Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
    {
      isPointingNull = false;
      Vector3 hitPosition = hit.point;

      //TODO: masih disimpan dalam x, y bukaan x, z
      GridPosition = new Vector3Int((int)hitPosition.x / GridConfig.CellSize.x, (int)hitPosition.z / GridConfig.CellSize.z, 0);
    }
    else
    {
      isPointingNull = true;
    }
  }
}
