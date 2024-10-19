using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;

public static class SaveSystem
{
  public static void SaveData(CharacterConfigSO playerData, ExpSO expData, List<UpgradableItemSO> upgradableItemDataList, DungeonSO dungeonData, GoldSO goldData)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/player.hh";
    FileStream steam = new FileStream(path, FileMode.Create);

    SaveableData data = new SaveableData(playerData, expData, upgradableItemDataList, dungeonData, goldData);

    formatter.Serialize(steam, data);
    steam.Close();
  }

  public static SaveableData LoadData()
  {
    string path = Application.persistentDataPath + "/player.hh";
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);

      SaveableData data = formatter.Deserialize(stream) as SaveableData;
      stream.Close();

      return data;
    }
    else
    {
      Debug.LogError("Save file not found in " + path);
      return null;
    }
  }

  public static bool CheckHasSaveData()
  {
    string path = Application.persistentDataPath + "/player.hh";

    return File.Exists(path);
  }
}
