using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeTrigger : MonoBehaviour
{
  public List<Collider> TargetList = new List<Collider>();
  public bool IsInAttackRange = false;
  public String TargetTag;
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag(TargetTag))
    {
      Debug.Log("ATTACK RANGE");
      TargetList.Add(other);
      IsInAttackRange = true;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag(TargetTag))
    {
      TargetList.Remove(other);
      IsInAttackRange = false;
    }
  }
}
