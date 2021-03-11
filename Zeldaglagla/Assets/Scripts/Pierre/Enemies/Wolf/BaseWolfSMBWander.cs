using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseWolfSMBWander : StateMachineBehaviour
{
    public BaseWolf baseWolf;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (baseWolf.isPackLeader)
        {
            Repath();
        } else
        {
            baseWolf.GetComponent<AIDestinationSetter>().target = baseWolf.pack.leader.transform;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (baseWolf.isPackLeader)
        {
            if (baseWolf.pather.remainingDistance < 2)
            {
                Repath();
            }
        } 
        else
        {
            bool tooclose = false;
            foreach (WolfRoot other in baseWolf.pack.wolves)
            {
                if (other != this.baseWolf)
                {
                    if (Vector2.Distance(other.transform.position, this.baseWolf.transform.position) < 1)
                    {
                        baseWolf.GetComponent<AIPath>().destination = this.baseWolf.transform.position + (this.baseWolf.transform.position - other.transform.position);
                        baseWolf.GetComponent<AIDestinationSetter>().enabled = false;
                        tooclose = true;
                    }
                }
            }
            if (!tooclose && !baseWolf.GetComponent<AIDestinationSetter>().enabled)
            {
                baseWolf.GetComponent<AIDestinationSetter>().enabled = true;
                baseWolf.GetComponent<AIDestinationSetter>().target = baseWolf.pack.leader.transform;
            }
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
        baseWolf.pather.destination = new Vector2(baseWolf.transform.position.x + rngx, baseWolf.transform.position.y + rngy);
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
