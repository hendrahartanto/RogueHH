using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPunchAnimationBehaviour : StateMachineBehaviour
{
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    int randomPunch = GlobalRandom.Next(0, 2);
    animator.SetInteger("RandomPunch", randomPunch);
  }
}
