using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentVariables : MonoBehaviour
{
  public CharacterConfigSO PlayerData;
  public ExpSO _playerExp = default;
  public List<UpgradableItemSO> UpgradableItemList = new List<UpgradableItemSO>();

  [Header("Listening on")]
  [SerializeField] private RequestSaveableDataEventChannelSO _requestSaveableDataEvent = default;

  private void OnEnable()
  {
    _requestSaveableDataEvent.PlayerData = PlayerData;
    _requestSaveableDataEvent.UpgradableItemDataList = UpgradableItemList;
    _requestSaveableDataEvent.ExpData = _playerExp;
  }

  private void OnDisable()
  {
    _requestSaveableDataEvent.PlayerData = null;
    _requestSaveableDataEvent.UpgradableItemDataList = null;
    _requestSaveableDataEvent.ExpData = null;
  }
}
