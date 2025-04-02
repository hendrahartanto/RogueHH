using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
  public int TargetFrameRate = 10;
  void Start()
  {
    Application.targetFrameRate = TargetFrameRate;
  }

}
