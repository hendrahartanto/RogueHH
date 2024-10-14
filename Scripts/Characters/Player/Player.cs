using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private GlobalMovementSpeedSO _movementSpeed;
  [SerializeField] private GameStateSO _gameState;
  [SerializeField] private CharacterConfigSO _playerConfigSO = default;
  [SerializeField] private BoolEventChannelSO _setActiveIsTurnCycling = default;
  public GameObject NormalArmor = default;
  public GameObject PlateArmor = default;
  public PathStorageSO PathStorage = default;
  public bool IsInAlert = false;
  public bool IsInCombat = false;
  public bool IsMoving = false;
  private bool _stopMovingFlag = false;
  public float MovementProgress = 0f;

  [Header("Broadcasting on")]
  [SerializeField] private VoidEventChannelSO _onTurnCycleExecuted = default;
  [SerializeField] private VoidEventChannelSO _reduceSkillTimeEvent = default;

  private void Awake()
  {
    PathStorage = ScriptableObject.CreateInstance<PathStorageSO>();
    SetupAppearance();
  }

  private IEnumerator MoveAlongPath()
  {
    List<Node> paths = new List<Node>(PathStorage.paths);

    int currentIndex = 0;

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

      if (currentIndex != paths.Count - 1)
        _reduceSkillTimeEvent.RaiseEvent();

      currentIndex++;
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

  private void SetupAppearance()
  {
    if (_playerConfigSO.appearanceType == AppearanceType.Normal)
    {
      PlateArmor.SetActive(false);
      NormalArmor.SetActive(true);
    }
    else if (_playerConfigSO.appearanceType == AppearanceType.Elite)
    {
      NormalArmor.SetActive(false);
      PlateArmor.SetActive(true);
    }
  }

  //called by animator event
  private void ActiveTurnCycling()
  {
    _setActiveIsTurnCycling.RaiseEvent(true);
  }

  private void DeactiveTurnCycling()
  {
    _setActiveIsTurnCycling.RaiseEvent(false);
  }

}
