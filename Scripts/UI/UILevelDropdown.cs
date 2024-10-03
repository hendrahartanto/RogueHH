using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UILevelDropdown : MonoBehaviour
{
  private TMP_Dropdown _dropdown = default;
  [SerializeField] private DungeonSO _dungeonSO = default;

  private void Start()
  {
    //clear untuk hapus reference dari gameplay sebelumnya
    _dungeonSO.rooms.Clear();

    _dropdown = GetComponent<TMP_Dropdown>();

    PopulateDropdown();
  }

  private void PopulateDropdown()
  {
    _dropdown.ClearOptions();

    List<string> options = new List<string>();

    options.Add("Boss");

    for (int i = 0; i <= _dungeonSO.MaxLevelReached; i++)
    {
      options.Add("Floor " + (i + 1));
    }

    _dropdown.AddOptions(options);

    _dropdown.value = _dungeonSO.MaxLevelReached + 1;
  }

  public void ChangeCurrentDungeonFloor(int index)
  {
    _dungeonSO.CurrentLevel = index - 1;
  }
}
