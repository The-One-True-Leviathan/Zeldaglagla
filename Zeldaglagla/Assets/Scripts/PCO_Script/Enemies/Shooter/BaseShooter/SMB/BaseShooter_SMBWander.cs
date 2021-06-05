using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooter_SMBWander : StateMachineBehaviour
{
    public BaseShooterBehavior shooter;
    bool cyclicPatrol, isCounting = false;
    public float reactionTime = 1;
    float timeElapsed, reactionTimeElapsed;
    Transform currentTarget;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shooter.baseShooterSMBState = BaseShooterBehavior.BaseShooterSMBState.WANDER;
        shooter.destinationSetter.enabled = true;

        shooter.pather.maxSpeed = shooter.walkSpeed;

        timeElapsed = 0;
        cyclicPatrol = shooter.cyclicPatrol;
        currentTarget = shooter.transform;
        float firstTargetDistance = Mathf.Infinity;
        foreach (Transform target in shooter.patrolTargets)
        {
            if ((target.position - shooter.transform.position).magnitude < firstTargetDistance)
            {
                currentTarget = target;
                firstTargetDistance = (target.position - shooter.transform.position).magnitude;
                //Debug.LogWarning("distance = " + firstTargetDistance);
            }
        }
        shooter.destinationSetter.target = currentTarget;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (shooter.SightCast())
        {
            reactionTimeElapsed += Time.deltaTime;
            if (reactionTimeElapsed > reactionTime)
                animator.Play("Approach");
        }
        else
        {
            reactionTimeElapsed -= Time.deltaTime;
            if (reactionTimeElapsed < 0)
                reactionTimeElapsed = 0;
        }

        if ((shooter.transform.position - currentTarget.position).magnitude < 1 && !isCounting)
        {
            //Debug.LogWarning("Searching new destination");
            NewDestination();
        }
        if (isCounting)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > shooter.patrolWaitTime)
            {
                isCounting = false;
                NewDestinationCoroutine();
            }
        }
    }

    void NewDestination()
    {
        //Debug.LogWarning(shooter.patrolTargets.IndexOf(currentTarget));
        isCounting = true;
        timeElapsed = 0;
    }

    void NewDestinationCoroutine()
    {
        if (shooter.randomPatrol)
        {
            List<Transform> possiblePoints = new List<Transform>();
            foreach (Transform point in shooter.patrolTargets)
            {
                if ((point.position - shooter.transform.position).magnitude < shooter.maxSight)
                {
                    possiblePoints.Add(point);
                }
            }
            int rng = UnityEngine.Random.Range(0, possiblePoints.Count);
            currentTarget = possiblePoints[rng];
        }
        if (shooter.patrolTargets.IndexOf(currentTarget) >= (shooter.patrolTargets.Count - 1))
        {
            //Debug.LogWarning("reached end of list");
            if (cyclicPatrol)
            {
                currentTarget = shooter.patrolTargets[0];
            }
            else
            {
                shooter.patrolTargets.Reverse();
                currentTarget = shooter.patrolTargets[0];
            }
        }
        else
        {
            currentTarget = shooter.patrolTargets[shooter.patrolTargets.IndexOf(currentTarget) + 1];

        }
        shooter.destinationSetter.target = currentTarget;
        //Debug.LogWarning(shooter.patrolTargets.IndexOf(currentTarget));
    }
}
