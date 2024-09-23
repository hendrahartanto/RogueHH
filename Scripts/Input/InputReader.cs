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
  private GameInput _gameInput;

  private void OnEnable()
  {
    if (_gameInput == null)
    {
      _gameInput = new GameInput();

      _gameInput.Gameplay.SetCallbacks(this);
      _gameInput.Gameplay.Enable();
    }
  }
  public void OnMouseClick(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && _gameState.CurrentGameState != GameState.TurnCycling)
      MouseClickEvent.Invoke();
  }

  public void DisableAllInput()
  {
    //TODO: add another input
    _gameInput.Gameplay.Disable();
  }
}
