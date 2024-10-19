using TMPro;
using UnityEngine;

public class UIGoldIndicatorController : MonoBehaviour
{
  [SerializeField] private GoldSO _goldSO = default;
  private TextMeshProUGUI _textAmount = default;

  [Header("Listening to")]
  [SerializeField] private IntEventChanelSO _updateGoldIndicatorUIEvent = default;
  [SerializeField] private VoidEventChannelSO _onCheatExecutedEvent = default;
  private void Awake()
  {
    _textAmount = GetComponentInChildren<TextMeshProUGUI>();
    SetGoldValueText(_goldSO.CurrentGold);
  }

  private void OnEnable()
  {
    _updateGoldIndicatorUIEvent.OnEventRaised += SetGoldValueText;
    _onCheatExecutedEvent.OnEventRaised += Refresh;
  }

  private void OnDisable()
  {
    _updateGoldIndicatorUIEvent.OnEventRaised -= SetGoldValueText;
    _onCheatExecutedEvent.OnEventRaised -= Refresh;
  }

  private void SetGoldValueText(int currentGold)
  {
    _textAmount.SetText(_goldSO.CurrentGold.ToString());
  }

  private void Refresh()
  {
    _textAmount.SetText(_goldSO.CurrentGold.ToString());
  }
}
