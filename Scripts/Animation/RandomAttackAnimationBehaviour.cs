using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAttackAnimationBehaviour : StateMachineBehaviour
{
  [SerializeField] private int _maxVariants;
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    int randomAttack = GlobalRandom.Next(0, _maxVariants);
    Debug.Log(randomAttack);
    animator.SetInteger("RandomAttack", randomAttack);
  }
}
