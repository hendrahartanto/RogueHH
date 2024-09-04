using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroTrigger : MonoBehaviour
{
  public bool IsReadyToChase = false;

  [Header("Broadcasting on")]
  [SerializeField] private TurnComponentEventChanelSO _onEnemyAggro = default;

  //Ketika player masuk zona agro musuh
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      var enemy = GetComponentInParent<Enemy>();
      _onEnemyAggro.RaiseEvent(enemy);
      IsReadyToChase = true;
    }
  }
}
