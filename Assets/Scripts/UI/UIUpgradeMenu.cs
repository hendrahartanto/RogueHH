using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIUpgradeMenu : MonoBehaviour
{
  [SerializeField] private Button _startGameplayButton = default;

  public UnityAction StartGameButtonAction;

  public void StartGameButtonOnClick()
  {
    StartGameButtonAction.Invoke();
  }
}
