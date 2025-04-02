using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event Channels/RequestSaveableDataEventChannelSO")]
public class RequestSaveableDataEventChannelSO : ScriptableObject
{
  public CharacterConfigSO PlayerData;
  public List<UpgradableItemSO> UpgradableItemDataList;
  public ExpSO ExpData;
  public DungeonSO DungeonData;
  public GoldSO GoldData;

  public CharacterConfigSO RequestPlayerData()
  {
    return PlayerData;
  }

  public List<UpgradableItemSO> RequestUpgradableItemDataList()
  {
    return UpgradableItemDataList;
  }

  public ExpSO RequestExpData()
  {
    return ExpData;
  }

  public DungeonSO RequestDungeonData()
  {
    return DungeonData;
  }

  public GoldSO RequestGoldData()
  {
    return GoldData;
  }
}
