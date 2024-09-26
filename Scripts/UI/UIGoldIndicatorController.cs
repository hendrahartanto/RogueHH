using TMPro;
using UnityEngine;

public class UIGoldIndicatorController : MonoBehaviour
{
  [SerializeField] private GoldSO _goldSO = default;
  private TextMeshProUGUI _textAmount = default;

  private void Awake()
  {
    _textAmount = GetComponentInChildren<TextMeshProUGUI>();
    SetInitialValue();
  }

  private void SetInitialValue()
  {
    _textAmount.SetText(_goldSO.CurrentGold.ToString());
  }
}
