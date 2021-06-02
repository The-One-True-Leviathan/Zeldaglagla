using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCO_BaseOrca_SMB_Circle : StateMachineBehaviour
{
    public PCO_BaseOrcaBehaviour baseOrca;
    bool reachedPoint;
    public int circlingTimeDiceAmmount;
    public float circiclingTimeDiceValue, circlingTimeModifier;
    float maxCirclingTime, circlingTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseOrca.destinationSetter.enabled = true;
        baseOrca.pather.enabled = true;
        reachedPoint = false;
        baseOrca.destinationSetter.target = baseOrca.circleCenterBehaviour.circlePointTransform;
        baseOrca.baseOrcaSMBState = PCO_BaseOrcaBehaviour.BaseOrcaSMBState.CIRCLE;
        baseOrca.pather.maxSpeed = baseOrca.runSpeed;
        for (int i = 0; i < circlingTimeDiceAmmount; i++)
        {
            maxCirclingTime += Random.Range(1, circiclingTimeDiceValue);
        }
        maxCirclingTime += circlingTimeModifier;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        baseOrca.SetAnim("Swim", baseOrca.pather.velocity);

        if (baseOrca.pather.remainingDistance < 2)
        {
            reachedPoint = true;
        }
        if (reachedPoint)
        {
            circlingTime += Time.deltaTime;
            if (circlingTime > maxCirclingTime)
            {
                animator.Play("Approach");
            }
        }
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
