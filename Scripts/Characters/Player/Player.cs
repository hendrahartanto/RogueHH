using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private GlobalMovementSpeedSO _movementSpeed;
  public PathStorageSO PathStorage = default;

  public bool IsMoving = false;
  private bool _stopMovingFlag = false;
  public float MovementProgress = 0f;

  private void Awake()
  {
    Debug.Log("Awake player");
    PathStorage = ScriptableObject.CreateInstance<PathStorageSO>();
  }

  private IEnumerator MoveAlongPath()
  {
    List<Node> paths = new List<Node>(PathStorage.paths);

    foreach (var path in paths)
    {
      Vector3 startPosition = transform.position;
      Vector3 endPosition = new Vector3(path.Position.x * GridConfig.CellSize.x, transform.position.y, path.Position.z * GridConfig.CellSize.z) + GridConfig.Offset;

      float distance = Vector3.Distance(startPosition, endPosition);
      MovementProgress = 0f;

      while (MovementProgress < 1f)
      {
        MovementProgress += _movementSpeed.MovementSpeed * Time.deltaTime / distance;

        transform.position = Vector3.Lerp(startPosition, endPosition, MovementProgress);

        yield return null;
      }

      transform.position = endPosition;

      //Cek jika ada perintah untuk berhenti bergerak
      if (_stopMovingFlag)
      {
        _stopMovingFlag = false;
        IsMoving = false;
        yield break; //untuk menghentikan coroutine
      }
    }

    IsMoving = false;
  }

  public void OnNotifyMovePlayer()
  {
    IsMoving = true;
    StartCoroutine(MoveAlongPath());
  }

  public void OnNotifyStopMoving()
  {
    _stopMovingFlag = true;
  }
}
