using System.Collections.Generic;

[System.Serializable]
public class SaveableData
{
  public PlayerData PlayerData;
  public List<UpgradableItemData> UpgradableItemDataList = new List<UpgradableItemData>();

  public SaveableData(CharacterConfigSO playerData, ExpSO expData, List<UpgradableItemSO> upgradableItemDataList)
  {
    PlayerData = new PlayerData
    {
      Level = playerData.Level,
      ExpCap = expData.ExpCap,
      CurrentExp = expData.CurrentExp
    };
    foreach (UpgradableItemSO item in upgradableItemDataList)
    {
      UpgradableItemDataList.Add(new UpgradableItemData
      {
        CurrentLevel = item.CurrentLevel,
        Price = item.Price
      });
    }
  }
}

[System.Serializable]
public class PlayerData
{
  public int Level;
  public int ExpCap;
  public int CurrentExp;
}


[System.Serializable]
public class UpgradableItemData
{
  public int CurrentLevel;
  public int Price;
}
