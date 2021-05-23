using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCO_BaseOrca_SMB_Approach : StateMachineBehaviour
{
    public PCO_BaseOrcaBehaviour baseOrca;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseOrca.destinationSetter.enabled = true;
        baseOrca.pather.enabled = true;
        baseOrca.destinationSetter.target = baseOrca.player.transform;
        baseOrca.baseOrcaSMBState = PCO_BaseOrcaBehaviour.BaseOrcaSMBState.APPROACH;
        baseOrca.pather.maxSpeed = baseOrca.runSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (baseOrca.pather.remainingDistance < baseOrca.attackDistance)
        {
            animator.Play("Attack");
        }
    }
}
