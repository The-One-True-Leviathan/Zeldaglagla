using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AlphaWolfSMBWander : StateMachineBehaviour
{
    public PCO_AlphaWolfBehavior baseWolf;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseWolf.destinationSetter.enabled = true;
        baseWolf.baseWolfSMBState = BaseWolf.BaseWolfSMBState.WANDER;
        baseWolf.pather.maxSpeed = baseWolf.wanderSpeed;
        if (baseWolf.isPackLeader)
        {
            Repath();
        } else
        {
            baseWolf.destinationSetter.target = baseWolf.pack.leader.transform;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(baseWolf.pather.velocity.magnitude > 0.05)
        {
            baseWolf.SetAnim("Walk", baseWolf.pather.velocity);
        } else
        {
            baseWolf.SetAnim("Idle", Vector3.down);
        }

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
                    if (Vector2.Distance(other.transform.position, baseWolf.transform.position) < 1)
                    {
                        baseWolf.transform.position += (this.baseWolf.transform.position - other.transform.position) * 1/ Vector2.Distance(other.transform.position, this.baseWolf.transform.position) * Time.deltaTime;
                        //baseWolf.destinationSetter.enabled = false;
                        tooclose = true;
                    }
                }
            }
            if (!tooclose && !baseWolf.destinationSetter.enabled)
            {
                baseWolf.destinationSetter.enabled = true;
                baseWolf.destinationSetter.target = baseWolf.pack.leader.transform;
            }
        }
        if(baseWolf.ToPlayer().magnitude < baseWolf.viewDistance)
        {
            if (baseWolf.packCircle.pack == null)
            {
                baseWolf.pack.AllGoToApproach();
            } else if (baseWolf.packCircle.pack != baseWolf)
            {
                baseWolf.packCircle.pack.AllGoToFlee();
                baseWolf.pack.AllGoToApproach();
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

        Vector3 newDestination = new Vector2(baseWolf.transform.position.x + rngx, baseWolf.transform.position.y + rngy);

        newDestination = Vector3.ClampMagnitude((newDestination - baseWolf.wanderAreaCenter.position), baseWolf.wanderAreaRadius) + baseWolf.wanderAreaCenter.position;

        baseWolf.pather.destination = newDestination;
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
