using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolWolfSMBWander : StateMachineBehaviour
{
    public PatrolWolf baseWolf;


    bool cyclicPatrol, isCounting = false;
    float timeElapsed;
    Transform currentTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseWolf.baseWolfSMBState = BaseWolf.BaseWolfSMBState.WANDER;
        baseWolf.destinationSetter.enabled = true;

        baseWolf.pather.maxSpeed = baseWolf.wanderSpeed;

        timeElapsed = 0;
        cyclicPatrol = baseWolf.cyclicPatrol;
        currentTarget = baseWolf.transform;
        float firstTargetDistance = Mathf.Infinity;
        foreach (Transform target in baseWolf.patrolTargets)
        {
            if ((target.position - baseWolf.transform.position).magnitude < firstTargetDistance)
            {
                currentTarget = target;
                firstTargetDistance = (target.position - baseWolf.transform.position).magnitude;
                //Debug.LogWarning("distance = " + firstTargetDistance);
            }
        }
        baseWolf.destinationSetter.target = currentTarget;

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

        if (baseWolf.SightCast())
        {
            if (baseWolf.player.GetComponent<PlayerPackCircle>().pack == null)
            {
                baseWolf.pack.AllGoToApproach();
            }
            else if (baseWolf.player.GetComponent<PlayerPackCircle>().pack != baseWolf)
            {
                baseWolf.pack.AllGoToFlee();
            }
        }

        if ((baseWolf.transform.position - currentTarget.position).magnitude < 1 && !isCounting)
        {
            //Debug.LogWarning("Searching new destination");
            NewDestination();
        }
        if (isCounting)
        {
            //Debug.LogWarning("Is Counting");
            timeElapsed += Time.deltaTime;
            if (timeElapsed > baseWolf.patrolWaitTime)
            {
                isCounting = false;
                NewDestinationCoroutine();
            }
        }
    }

    void NewDestination()
    {
        //Debug.LogWarning(baseWolf.patrolTargets.IndexOf(currentTarget));
        isCounting = true;
        timeElapsed = 0;
    }

    void NewDestinationCoroutine()
    {
        if (baseWolf.patrolTargets.IndexOf(currentTarget) >= (baseWolf.patrolTargets.Count - 1))
        {
            //Debug.LogWarning("reached end of list");
            if (cyclicPatrol)
            {
                currentTarget = baseWolf.patrolTargets[0];
            }
            else
            {
                baseWolf.patrolTargets.Reverse();
                currentTarget = baseWolf.patrolTargets[0];
            }
        }
        else
        {
            currentTarget = baseWolf.patrolTargets[baseWolf.patrolTargets.IndexOf(currentTarget) + 1];

        }
        baseWolf.destinationSetter.target = currentTarget;
        //Debug.LogWarning(baseWolf.patrolTargets.IndexOf(currentTarget));
    }
}
