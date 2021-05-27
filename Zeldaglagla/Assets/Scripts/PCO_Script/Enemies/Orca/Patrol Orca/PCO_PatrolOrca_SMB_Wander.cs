using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCO_PatrolOrca_SMB_Wander : StateMachineBehaviour
{
    public PCO_PatrolOrcaBehaviour baseOrca;
    bool cyclicPatrol, isCounting = false;
    float timeElapsed;
    Transform currentTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseOrca.destinationSetter.enabled = true;
        baseOrca.pather.enabled = true;
        baseOrca.baseOrcaSMBState = PCO_BaseOrcaBehaviour.BaseOrcaSMBState.WANDER;
        baseOrca.destinationSetter.enabled = true;

        baseOrca.pather.maxSpeed = baseOrca.walkSpeed;

        timeElapsed = 0;
        cyclicPatrol = baseOrca.cyclicPatrol;
        currentTarget = baseOrca.transform;
        float firstTargetDistance = Mathf.Infinity;
        foreach (Transform target in baseOrca.patrolPoints)
        {
            if ((target.position - baseOrca.transform.position).magnitude < firstTargetDistance)
            {
                currentTarget = target;
                firstTargetDistance = (target.position - baseOrca.transform.position).magnitude;
                //Debug.LogWarning("distance = " + firstTargetDistance);
            }
        }
        baseOrca.destinationSetter.target = currentTarget;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (baseOrca.SightCast())
        {
            baseOrca.StartCircling();
        }

        if ((baseOrca.transform.position - currentTarget.position).magnitude < 1 && !isCounting)
        {
            //Debug.LogWarning("Searching new destination");
            NewDestination();
        }
        if (isCounting)
        {
            //Debug.LogWarning("Is Counting");
            timeElapsed += Time.deltaTime;
            if (timeElapsed > baseOrca.patrolWaitTime)
            {
                isCounting = false;
                NewDestinationCoroutine();
            }
        }
    }

    void NewDestination()
    {
        //Debug.LogWarning(baseOrca.patrolPoints.IndexOf(currentTarget));
        isCounting = true;
        timeElapsed = 0;
    }

    void NewDestinationCoroutine()
    {
        if (baseOrca.patrolPoints.IndexOf(currentTarget) >= (baseOrca.patrolPoints.Count - 1))
        {
            //Debug.LogWarning("reached end of list");
            if (cyclicPatrol)
            {
                currentTarget = baseOrca.patrolPoints[0];
            }
            else
            {
                baseOrca.patrolPoints.Reverse();
                currentTarget = baseOrca.patrolPoints[0];
            }
        }
        else
        {
            currentTarget = baseOrca.patrolPoints[baseOrca.patrolPoints.IndexOf(currentTarget) + 1];

        }
        baseOrca.destinationSetter.target = currentTarget;
        //Debug.LogWarning(baseOrca.patrolPoints.IndexOf(currentTarget));
    }
}

