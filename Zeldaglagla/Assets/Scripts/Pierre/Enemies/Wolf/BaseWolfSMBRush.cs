using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseWolfSMBRush : StateMachineBehaviour
{
    public BaseWolf baseWolf;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseWolf.baseWolfSMBState = BaseWolf.BaseWolfSMBState.RUSH;
        baseWolf.pather.maxSpeed = baseWolf.rushSpeed;
        Debug.LogWarning("HEOOOO");
        baseWolf.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.LogWarning("Attached");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(baseWolf.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < baseWolf.attackDistance)
        {
            baseWolf.pack.StartHarassing();
            baseWolf.pack.SetNextAttack();
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
