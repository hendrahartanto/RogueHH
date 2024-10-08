using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
  [SerializeField] private GameStateSO _gameState = default;
  public event UnityAction MouseClickEvent = delegate { };
  public event UnityAction KeyboardSpaceEvent = delegate { };
  public event UnityAction Skill1Action = delegate { };
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
      _gameInput.Gameplay.Enable();
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
    if (context.phase == InputActionPhase.Performed && !_gameState.IsTurnCycling && _gameState.CurrentGameState != GameState.Gameover)
      KeyboardSpaceEvent.Invoke();
  }

  public void OnKeyboard_1(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && !_gameState.IsTurnCycling && _gameState.CurrentGameState != GameState.Gameover)
      Skill1Action.Invoke();
  }

  public void DisableAllInput()
  {
    MouseClickEvent = null;
    MouseClickEvent = delegate { };

    Skill1Action = delegate { };

    _gameInput.Gameplay.Disable();
  }

  public void EnableGameplayInput()
  {
    _gameInput.Gameplay.Enable();
  }
}
