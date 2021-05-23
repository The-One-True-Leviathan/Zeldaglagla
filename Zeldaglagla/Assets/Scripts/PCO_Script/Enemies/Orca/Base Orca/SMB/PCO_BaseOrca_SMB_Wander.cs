using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCO_BaseOrca_SMB_Wander : StateMachineBehaviour
{
    public PCO_BaseOrcaBehaviour baseOrca;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseOrca.destinationSetter.enabled = true;
        baseOrca.pather.enabled = true;
        baseOrca.baseOrcaSMBState = PCO_BaseOrcaBehaviour.BaseOrcaSMBState.WANDER;
        baseOrca.pather.maxSpeed = baseOrca.walkSpeed;
        Repath();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (baseOrca.pather.remainingDistance < 2)
        {
            Repath();
        }
        if (baseOrca.ToPlayer().magnitude < baseOrca.viewDistance)
        {
            baseOrca.StartCircling();
        }
    }

    void Repath()
    {
        float rngx = (Random.Range(10f, 20f) + Random.Range(10f, 20f)) / 2;
        if (Random.Range(0f, 1f) < 0.5)
        {
            rngx *= -1;
        }
        float rngy = (Random.Range(10f, 20f) + Random.Range(10f, 20f)) / 2;
        if (Random.Range(0f, 1f) < 0.5)
        {
            rngy *= -1;
        }
        baseOrca.pather.destination = new Vector2(baseOrca.transform.position.x + rngx, baseOrca.transform.position.y + rngy);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
