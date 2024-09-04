using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, ITurnComponent
{
  public GlobalMovementSpeedSO MovementSpeed;
  public PathStorageSO PathStorage;
  public bool IsReadyToChase = false;
  public bool IsMoving = false;
  public float MovementProgress = 0f;

  [Header("Broadcasting on")]
  [SerializeField] private TurnComponentEventChanelSO _onEnemyAggro = default;
  [SerializeField] private VoidEventChannelSO _onTurnFinished = default;


  public UnityAction OnTurnExecuted { get; set; }

  private IEnumerator MoveToTarget()
  {
    Node target = PathStorage.paths[0];

    Vector3 startPosition = transform.position;
    Vector3 endPosition = new Vector3(target.Position.x * GridConfig.CellSize.x, transform.position.y, target.Position.z * GridConfig.CellSize.z) + GridConfig.Offset;

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

    IsMoving = false;
    _onTurnFinished.RaiseEvent();
  }

  public void OnNotifyMoveEnemy()
  {
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

  //Ketika player masuk zona agro musuh
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      _onEnemyAggro.RaiseEvent(this);
      IsReadyToChase = true;
    }
  }
}
