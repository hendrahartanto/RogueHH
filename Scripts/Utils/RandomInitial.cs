using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInitial
{
  private static List<string> _initials = new List<string> { "HH", "VR", "VK", "IS", "DT", "MF", "AI", "LM", "NL", "DE", "JZ", "XK", "FD", "AY", "BL", "MH", "RJ" };

  private static List<Color> _rarityColors = new List<Color> { Color.white, Color.yellow, Color.red };

  public static string GetRandomInitial()
  {
    return _initials[Random.Range(0, _initials.Count)];
  }

  public static Color GetRarityColor(int type)
  {
    return _rarityColors[type];
  }
}
