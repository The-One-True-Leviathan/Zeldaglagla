using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseWolfSMBHarass : StateMachineBehaviour
{
    public BaseWolf baseWolf;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseWolf = animator.GetComponent<BaseWolf>();
        baseWolf.baseWolfSMBState = BaseWolf.BaseWolfSMBState.HARASS;
        baseWolf.pather.maxSpeed = baseWolf.observeSpeed;
        Debug.LogWarning("HEOOOO");
        foreach (GameObject holder in GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPackCircle>().holders)
        {
            if (holder.GetComponent<WolfHolder>().wolf == baseWolf.gameObject)
            {
                baseWolf.destinationSetter.target = holder.transform;
                Debug.LogWarning("Attached");
                break;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (baseWolf.pather.velocity.magnitude > 0.05)
        {
            baseWolf.SetAnim("Walk", baseWolf.pather.velocity);
        }
        else
        {
            baseWolf.SetAnim("Idle", Vector3.down);
        }
        if (baseWolf.pack.wolves.Count == 1)
        {
            animator.Play("Attack");
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
