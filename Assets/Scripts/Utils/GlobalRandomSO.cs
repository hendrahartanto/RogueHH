using UnityEngine;
using System;

public static class GlobalRandom
{
  private static System.Random random = new System.Random();

  public static int Next(int min, int max)
  {
    return random.Next(min, max);
  }
}
