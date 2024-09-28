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
  public Collider CurrentPointedCollider = default;
  private bool _isRaycastEnabled = true;
  [SerializeField] private PointerStateSO _pointerState;
  [SerializeField] private Camera _mainCamera;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _onPlayerInstantiated = default;
  [SerializeField] private BoolEventChannelSO _raycastSetActiveEvent = default;

  private void SetupCamera()
  {
    GameObject mainCamera = GameObject.Find("Main Camera");
    _mainCamera = mainCamera.GetComponent<Camera>();
  }

  private void OnEnable()
  {
    _onPlayerInstantiated.OnEventRaised += SetupCamera;
    _raycastSetActiveEvent.OnEventRaised += SetRaycastActive;
  }

  private void OnDisable()
  {
    _onPlayerInstantiated.OnEventRaised -= SetupCamera;
    _raycastSetActiveEvent.OnEventRaised -= SetRaycastActive;
  }

  private void Update()
  {
    if (!_isRaycastEnabled)
      return;

    Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
    RaycastHit hit;
    bool isHit = false;

    while (!isHit)
    {
      if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
      {
        // Check if the hit object has the tag "Player"
        if (hit.collider.CompareTag("Player"))
        {
          ray.origin = hit.point + ray.direction * 0.01f;
          continue;
        }

        isPointingNull = false;
        Vector3 hitPosition = hit.point;

        CurrentPointedCollider = hit.collider;

        // TODO: Still using x, y instead of x, z for GridPosition
        GridPosition = new Vector3Int((int)hitPosition.x / GridConfig.CellSize.x, (int)hitPosition.z / GridConfig.CellSize.z, 0);

        isHit = true;
      }
      else
      {
        CurrentPointedCollider = null;
        isPointingNull = true;
        break;
      }
    }
  }

  private void SetRaycastActive(bool isActive)
  {
    _isRaycastEnabled = isActive;
  }
}
