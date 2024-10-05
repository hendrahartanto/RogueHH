using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAggroTrigger : MonoBehaviour
{
  public bool IsReadyToChase = false;
  public bool IsEnemyAlert = false;
  private Enemy _enemy = default;
  [SerializeField] private TransformAnchorSO _currentPlayerPosition = default;

  [Header("Grid")]
  [SerializeField] private GridNodeSO _gridNode;

  [Header("Broadcasting on")]
  [SerializeField] private TurnComponentEventChanelSO _onEnemyAggro = default;
  [SerializeField] private TurnComponentEventChanelSO _removeEnemyFromQueueEvent = default;
  [SerializeField] private VoidEventChannelSO _onTurnFinished = default;
  [SerializeField] private TextPopupEventChannelSO _textPopupEvent = default;
  [SerializeField] private GameStateEventChanelSO _changeGameStateEvent = default;
  [SerializeField] private PlaySFXEventChannelSO _playSFXEvent = default;

  private void Awake()
  {
    _enemy = GetComponentInParent<Enemy>();
  }

  //Ketika player masuk zona agro musuh
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      IsEnemyAlert = true;

      //masukin ke queue walaupun ga ke aggro agar di recheck terus LOS nya
      _onEnemyAggro.RaiseEvent(_enemy);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      IsEnemyAlert = false;

      if (!IsReadyToChase)
        _removeEnemyFromQueueEvent.RaiseEvent(_enemy);
    }
  }

  public void CheckAggro()
  {
    var playerPosition = _currentPlayerPosition.Value.position;
    var enemyPosition = transform.position;

    if (IsLineOfSightClear(playerPosition, enemyPosition))
    {
      _playSFXEvent.RaiseEvent(SFXName.Alert, transform);
      _textPopupEvent.RaiseEvent(transform.position, "!!", TextColor.Red);
      _changeGameStateEvent.RaiseEvent(GameState.Combat);
      StartCoroutine(RotateTowardTarget(_currentPlayerPosition.Value));
    }
    else
      _onTurnFinished.RaiseEvent();
  }

  private bool IsLineOfSightClear(Vector3 start, Vector3 end)
  {
    Vector3 direction = (end - start).normalized;

    float stepSize = GridConfig.CellSize.x * 0.25f; // stepnya dikecilin agar pengecekan LOS lebih akurat

    Vector3 currentPos = start;
    while (Vector3.Distance(currentPos, end) > stepSize)
    {

      Vector2Int gridPos = new Vector2Int(
          Mathf.FloorToInt(currentPos.x / GridConfig.CellSize.x),
          Mathf.FloorToInt(currentPos.z / GridConfig.CellSize.z)
      );

      if (!_gridNode.InBounds(gridPos) || _gridNode[gridPos] == null)
      {
        return false;
      }

      currentPos += direction * stepSize;
    }

    return true;
  }

  private IEnumerator RotateTowardTarget(Transform targetTransform)
  {
    Vector3 directionToTarget = (targetTransform.position - _enemy.transform.position).normalized;

    //rotate
    if (directionToTarget != Vector3.zero)
    {
      Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

      //TODO: set using configSO
      float rotationSpeed = 40f;
      while (Quaternion.Angle(_enemy.transform.rotation, targetRotation) > 0.1f)
      {
        _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        yield return null;
      }

      _enemy.transform.rotation = targetRotation;
    }
    IsReadyToChase = true;
    _onTurnFinished.RaiseEvent();
  }
}
