using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, ITurnComponent
{
  public GlobalMovementSpeedSO MovementSpeed;
  public PathStorageSO PathStorage;
  public bool IsMoving = false;
  public float MovementProgress = 0f;
  [SerializeField] private GridNodeSO _gridNode = default;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _onTurnFinished = default;
  [SerializeField] private ChangeCellTypeEventChanel _changeCellTypeEvent = default;
  [SerializeField] private GridNodeBoolEventChanelSO _changeNodeAccessibleEvent = default;
  [SerializeField] private VoidEventChannelSO _recalculatePathEvent = default;

  public UnityAction OnTurnExecuted { get; set; }

  private IEnumerator MoveToTarget()
  {

    Node target = PathStorage.paths[0];

    Debug.Log("path from enemy: " + target.Position);

    Vector3 startPosition = transform.position;
    Vector3 endPosition = new Vector3(target.Position.x * GridConfig.CellSize.x, transform.position.y, target.Position.z * GridConfig.CellSize.z) + GridConfig.Offset;

    int tempX = (int)transform.position.x / GridConfig.CellSize.x;
    int tempZ = (int)transform.position.z / GridConfig.CellSize.z;

    float distance = Vector3.Distance(startPosition, endPosition);
    MovementProgress = 0f;

    //cek arah rotasi
    Vector3 direction = (endPosition - startPosition).normalized;

    //rotate
    if (direction != Vector3.zero)
    {
      Quaternion targetRotation = Quaternion.LookRotation(direction);

      float rotationSpeed = 50f;
      while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
      {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        yield return null;
      }

      transform.rotation = targetRotation;
    }

    while (MovementProgress < 1f)
    {
      MovementProgress += MovementSpeed.MovementSpeed * Time.deltaTime / distance;

      transform.position = Vector3.Lerp(startPosition, endPosition, MovementProgress);

      yield return null;
    }

    transform.position = endPosition;

    //ganti start position jadi walkable
    _changeCellTypeEvent.RaiseEvent(tempX, tempZ, CellType.Walkable);
    _gridNode[tempX, tempZ].Accessable = true;

    //ganti end position jadi enemy
    _changeCellTypeEvent.RaiseEvent(target.Position.x, target.Position.z, CellType.Enemy);
    _changeNodeAccessibleEvent.RaiseEvent(target.Position.x, target.Position.z, false);

    IsMoving = false;
    _recalculatePathEvent.RaiseEvent();
    _onTurnFinished.RaiseEvent();
  }

  public void OnNotifyMoveEnemy()
  {
    if (PathStorage.paths.Count <= 0)
    {
      _onTurnFinished.RaiseEvent();
      return;
    }

    IsMoving = true;

    StartCoroutine(MoveToTarget());
  }

  public void ExecuteTurn()
  {
    OnTurnExecuted?.Invoke();
  }

  private void Awake()
  {
    PathStorage = ScriptableObject.CreateInstance<PathStorageSO>();
  }
}
