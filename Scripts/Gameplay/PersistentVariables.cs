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
  public List<VoidEventChannelSO> CodeActionList = default;

  private void OnEnable()
  {
    _requestSaveableDataEvent.PlayerData = PlayerData;
    _requestSaveableDataEvent.UpgradableItemDataList = UpgradableItemList;
    _requestSaveableDataEvent.ExpData = _playerExp;
    _requestSaveableDataEvent.DungeonData = DungeonData;
    _requestSaveableDataEvent.GoldData = GoldData;

    CodeActionList[0].OnEventRaised += IncreaseMoney;
    CodeActionList[1].OnEventRaised += IncreaseExp;
    CodeActionList[2].OnEventRaised += UnlockDungeonLevel;

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

    CodeActionList[0].OnEventRaised -= IncreaseMoney;
    CodeActionList[1].OnEventRaised -= IncreaseExp;
    CodeActionList[2].OnEventRaised -= UnlockDungeonLevel;

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
    skillContainerSO.Reset();
    SaveableData loadData = SaveSystem.LoadData();
    PlayerData.Level = loadData.PlayerData.Level;
    PlayerData.ExpCap = loadData.PlayerData.ExpCap;
    _playerExp.SetCurrentExp(loadData.PlayerData.CurrentExp);
    _playerExp.SetExpCap(loadData.PlayerData.ExpCap);

    for (int i = 0; i < UpgradableItemList.Count; i++)
    {
      UpgradableItemList[i].SetLevel(loadData.UpgradableItemDataList[i].CurrentLevel);
      UpgradableItemList[i].SetPrice(loadData.UpgradableItemDataList[i].Price);
    }

    DungeonData.CurrentLevel = loadData.DungeonData.CurrentLevel;
    DungeonData.MaxLevelReached = loadData.DungeonData.MaxLevelReached;

    GoldData.SetGold(loadData.GoldData.Gold);
  }

  private void IncreaseMoney()
  {
    GoldData.IncreaseGold(20000);
  }

  private void IncreaseExp()
  {
    _playerExp.GainExp(1000);

    if (_playerExp.CurrentExp >= _playerExp.ExpCap)
    {
      OnLevelUp();
    }
  }

  public void SetupStats(int remainingExp)
  {
    float levelScaling = Mathf.Pow(PlayerData.Level, 1.75f);
    float expCapMultiplier = 10 + (2 * PlayerData.Level);

    _playerExp.SetExpCap((int)(_playerExp.ExpCap + expCapMultiplier + levelScaling));

    _playerExp.SetExpCap(_playerExp.ExpCap);
    _playerExp.SetCurrentExp(remainingExp);
  }

  private void OnLevelUp()
  {
    int remainingExp = _playerExp.CurrentExp;

    while (remainingExp >= _playerExp.ExpCap)
    {
      remainingExp -= _playerExp.ExpCap;

      PlayerData.Level++;

      SetupStats(remainingExp);
    }
  }

  private void UnlockDungeonLevel()
  {
    DungeonData.MaxLevelReached = 100;
    DungeonData.CurrentLevel = 100;
  }
}
