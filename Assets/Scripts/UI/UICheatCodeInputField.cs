using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICheatCodeInputField : MonoBehaviour
{
  public TMP_InputField InputField;
  public List<string> CodeList;

  [Header("Broadcasting to")]
  public List<VoidEventChannelSO> CodeActionList = default;
  [SerializeField] private VoidEventChannelSO _onCheatExecutedEvent = default;
  [SerializeField] private PlaySFXEventChannelSO _playSFXEvent = default;

  public void ValidateCode()
  {
    string inputText = InputField.text;

    for (int i = 0; i < CodeList.Count; i++)
    {
      if (CodeList[i] == inputText)
      {
        CodeActionList[i]?.RaiseEvent();
        InputField.text = "";

        _onCheatExecutedEvent.RaiseEvent();
        _playSFXEvent.RaiseEvent(SFXName.CheatActive, transform);
      }
    }
  }
}
