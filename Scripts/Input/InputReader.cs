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
  private GameInput _gameInput;

  [Header("Listening to")]
  [SerializeField] private VoidEventChannelSO _enableGameplayInputEvent = default;
  [SerializeField] private VoidEventChannelSO _disableAllInputEvent = default;

  private void OnEnable()
  {
    if (_gameInput == null)
    {
      _gameInput = new GameInput();

      _gameInput.Gameplay.SetCallbacks(this);
      _gameInput.Gameplay.Enable();
    }

    _enableGameplayInputEvent.OnEventRaised += EnableGameplayInput;
    _disableAllInputEvent.OnEventRaised += DisableAllInput;
  }

  private void OnDisable()
  {
    _enableGameplayInputEvent.OnEventRaised -= EnableGameplayInput;
    _disableAllInputEvent.OnEventRaised -= DisableAllInput;
  }

  public void OnMouseClick(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && _gameState.CurrentGameState != GameState.TurnCycling && _gameState.CurrentGameState != GameState.Gameover)
      MouseClickEvent.Invoke();
  }

  public void OnKeyboardSpace(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && _gameState.CurrentGameState != GameState.TurnCycling && _gameState.CurrentGameState != GameState.Gameover)
      KeyboardSpaceEvent.Invoke();
  }

  public void DisableAllInput()
  {
    RemoveAllInputAction();
    _gameInput.Gameplay.Disable();
  }

  public void EnableGameplayInput()
  {
    _gameInput.Gameplay.Enable();
  }

  private void RemoveAllInputAction()
  {
    MouseClickEvent = null;
    KeyboardSpaceEvent = null;
  }

}
