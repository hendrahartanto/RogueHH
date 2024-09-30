using System;
using UnityEngine;

[CreateAssetMenu(menuName = "UpgradeMenuItem/UpgradableItem")]
public class UpgradableItemSO : ScriptableObject
{
  [SerializeField] private CharacterConfigSO _playerStats = default;
  [SerializeField] private Sprite _iconImage = default;
  [SerializeField] private String _name = default;
  [SerializeField] private String _description = default;
  [SerializeField] private int _price = default;
  [SerializeField] private float _upgradeValue = default; //value stat yang akan ditambahin untuk upgrade
  [SerializeField] private UpgradeItemType _type = default;
  [SerializeField] private int _currentLevel = default;
  [SerializeField] private int _maxLevel = default;

  public CharacterConfigSO PlayerStats => _playerStats;
  public Sprite IconImage => _iconImage;
  public String Name => _name;
  public String Description => _description;
  public int Price => _price;
  public float UpgradeValue => _upgradeValue;
  public UpgradeItemType Type => _type;
  public int CurrentLevel => _currentLevel;
  public int MaxLevel => _maxLevel;

  public void IncreasePrice(int value)
  {
    _price += value;
  }

  public void IncrementLevel()
  {
    _currentLevel++;
  }
}
