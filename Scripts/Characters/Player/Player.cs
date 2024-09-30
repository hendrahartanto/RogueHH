using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private GlobalMovementSpeedSO _movementSpeed;
  [SerializeField] private GameStateSO _gameState;
  [SerializeField] private CharacterConfigSO _playerConfigSO = default;
  public PathStorageSO PathStorage = default;
  public bool IsInAlert = false;
  public bool IsInCombat = false;
  public bool IsMoving = false;
  private bool _stopMovingFlag = false;
  public float MovementProgress = 0f;
  private Animator _animator;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _onTurnCycleExecuted = default;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _resetPlayerLevelEvent = default;

  private void OnEnable()
  {
    if (_resetPlayerLevelEvent != null)
      _resetPlayerLevelEvent.OnEventRaised += ResetPlayerLevel;
  }

  private void OnDisable()
  {
    if (_resetPlayerLevelEvent != null)
      _resetPlayerLevelEvent.OnEventRaised -= ResetPlayerLevel;
  }

  private void Awake()
  {
    PathStorage = ScriptableObject.CreateInstance<PathStorageSO>();
    _animator = GetComponent<Animator>();
    SetWeaponType();
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
        MovementProgress += _movementSpeed.MovementSpeed * Time.deltaTime / distance;

        transform.position = Vector3.Lerp(startPosition, endPosition, MovementProgress);

        yield return null;
      }

      transform.position = endPosition;

      //Cek jika ada perintah untuk berhenti bergerak atau Cek jika lagi dalam combat (hanya bisa move 1 tile per turn)
      IsInAlert = _gameState.CurrentGameState == GameState.Alert;
      IsInCombat = _gameState.CurrentGameState == GameState.Combat;

      if (_stopMovingFlag || IsInAlert || IsInCombat)
      {
        _stopMovingFlag = false;
        IsMoving = false;

        _onTurnCycleExecuted.RaiseEvent();

        yield break; //untuk menghentikan coroutine
      }

    }

    _onTurnCycleExecuted.RaiseEvent();

    IsMoving = false;
  }

  private void ResetPlayerLevel()
  {
    _playerConfigSO.Level = 0;
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

  private void SetWeaponType()
  {
    _animator.SetInteger("WeaponType", _playerConfigSO.WeaponType);
  }
}
