using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeTrigger : MonoBehaviour
{
  public bool IsInAttackRange = false;
  public String TargetTag;
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag(TargetTag))
    {
      IsInAttackRange = true;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag(TargetTag))
    {
      IsInAttackRange = false;
    }
  }
}
