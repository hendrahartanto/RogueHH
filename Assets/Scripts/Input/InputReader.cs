using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IPauseActions
{
  [SerializeField] private GameStateSO _gameState = default;
  public event UnityAction MouseClickEvent = delegate { };
  public event UnityAction KeyboardSpaceEvent = delegate { };
  public event UnityAction Skill1Action = delegate { };
  public event UnityAction Skill2Action = delegate { };
  public event UnityAction Skill3Action = delegate { };
  public event UnityAction KeyboardEscAction = delegate { };

  public float spaceCooldown = 0.5f;
  private float lastSpacePressTime = -Mathf.Infinity;

  private GameInput _gameInput;
  public bool StopPlayerMovementOnClick = false;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _enableGameplayInputEvent = default;

  private void OnEnable()
  {
    if (_gameInput == null)
    {
      _gameInput = new GameInput();

      _gameInput.Gameplay.SetCallbacks(this);
      _gameInput.Pause.SetCallbacks(this);
      _gameInput.Gameplay.Enable();
      _gameInput.Pause.Enable();
    }

    _enableGameplayInputEvent.OnEventRaised += EnableGameplayInput;
  }

  private void OnDisable()
  {
    _enableGameplayInputEvent.OnEventRaised -= EnableGameplayInput;
  }

  public void OnMouseClick(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && !_gameState.IsTurnCycling && _gameState.CurrentGameState != GameState.Gameover)
      MouseClickEvent.Invoke();
    else if (context.phase == InputActionPhase.Performed && StopPlayerMovementOnClick)
      MouseClickEvent.Invoke();
  }

  public void OnKeyboardSpace(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed
        && !_gameState.IsTurnCycling
        && _gameState.CurrentGameState != GameState.Gameover)
    {
      if (Time.time - lastSpacePressTime >= spaceCooldown)
      {
        lastSpacePressTime = Time.time;
        KeyboardSpaceEvent.Invoke();
      }
    }
  }

  public void OnKeyboard_1(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && !_gameState.IsTurnCycling && _gameState.CurrentGameState != GameState.Gameover)
      Skill1Action.Invoke();
  }

  public void OnKeyboard_2(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && !_gameState.IsTurnCycling && _gameState.CurrentGameState != GameState.Gameover)
      Skill2Action.Invoke();
  }

  public void OnKeyboard_3(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && !_gameState.IsTurnCycling && _gameState.CurrentGameState != GameState.Gameover)
      Skill3Action.Invoke();
  }

  public void OnKeyboard_esc(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && _gameState.CurrentGameState != GameState.Gameover)
      KeyboardEscAction.Invoke();
  }

  public void DisableAllInput()
  {
    RemoveInputAction();

    _gameInput.Gameplay.Disable();
    _gameInput.Pause.Disable();
  }

  private void RemoveInputAction()
  {
    MouseClickEvent = null;
    MouseClickEvent = delegate { };

    Skill1Action = delegate { };
    Skill2Action = delegate { };
    Skill3Action = delegate { };
  }

  public void ToggleGameplayInput()
  {
    if (_gameInput.Gameplay.enabled)
      _gameInput.Gameplay.Disable();
    else
      _gameInput.Gameplay.Enable();
  }

  public void EnableGameplayInput()
  {
    _gameInput.Gameplay.Enable();
    _gameInput.Pause.Enable();
  }
}
