using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeMenuItem/UpgradableItem")]
public class UpgradableItemSO : ScriptableObject
{
  public CharacterConfigSO PlayerStats = default;
  public Sprite IconImage = default;
  public String Name = default;
  public String Description = default;
  public int Price = default;
  public float UpgradeValue = default; //value stat yang akan ditambahin untuk upgrade
  public UpgradeItemType Type = default;
  public int CurrentLevel = default;

  private void Awake()
  {

  }
}
