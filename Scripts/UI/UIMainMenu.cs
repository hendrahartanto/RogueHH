using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
  [SerializeField] private Button _newGameButton = default;

  public UnityAction NewGameButtonAction;

  public void OnClickNewGameButton()
  {
    NewGameButtonAction.Invoke();
  }
}
