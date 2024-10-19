using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentVariables : MonoBehaviour
{
  public CharacterConfigSO PlayerData;
  public ExpSO _playerExp = default;
  public List<UpgradableItemSO> UpgradableItemList = new List<UpgradableItemSO>();
  public DungeonSO DungeonData = default;
  public GoldSO GoldData = default;
  public SkillContainerSO skillContainerSO = default;

  [Header("Listening on")]
  [SerializeField] private RequestSaveableDataEventChannelSO _requestSaveableDataEvent = default;
  [SerializeField] private VoidEventChannelSO _resetSaveableDataEvent = default;
  [SerializeField] private VoidEventChannelSO _loadSaveableDataEvent = default;

  private void OnEnable()
  {
    _requestSaveableDataEvent.PlayerData = PlayerData;
    _requestSaveableDataEvent.UpgradableItemDataList = UpgradableItemList;
    _requestSaveableDataEvent.ExpData = _playerExp;
    _requestSaveableDataEvent.DungeonData = DungeonData;
    _requestSaveableDataEvent.GoldData = GoldData;

    _resetSaveableDataEvent.OnEventRaised += ResetData;
    _loadSaveableDataEvent.OnEventRaised += LoadData;
  }

  private void OnDisable()
  {
    _requestSaveableDataEvent.PlayerData = null;
    _requestSaveableDataEvent.UpgradableItemDataList = null;
    _requestSaveableDataEvent.ExpData = null;
    _requestSaveableDataEvent.DungeonData = null;
    _requestSaveableDataEvent.GoldData = null;

    _resetSaveableDataEvent.OnEventRaised -= ResetData;
    _loadSaveableDataEvent.OnEventRaised -= LoadData;
  }

  private void ResetData()
  {
    PlayerData.Reset();
    _playerExp.Reset();
    foreach (UpgradableItemSO item in UpgradableItemList)
    {
      item.Reset();
    }
    DungeonData.Reset();
    GoldData.Reset();
    skillContainerSO.Reset();
  }

  private void LoadData()
  {
    SaveableData loadData = SaveSystem.LoadData();
    PlayerData.Level = loadData.PlayerData.Level;
    PlayerData.ExpCap = loadData.PlayerData.ExpCap;
    _playerExp.SetCurrentExp(loadData.PlayerData.CurrentExp);

    for (int i = 0; i < UpgradableItemList.Count; i++)
    {
      UpgradableItemList[i].SetLevel(loadData.UpgradableItemDataList[i].CurrentLevel);
      UpgradableItemList[i].SetPrice(loadData.UpgradableItemDataList[i].Price);
    }

    DungeonData.CurrentLevel = loadData.DungeonData.CurrentLevel;
    DungeonData.MaxLevelReached = loadData.DungeonData.MaxLevelReached;
  }
}
