using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;

public class BaseWolfSMBFlee : StateMachineBehaviour
{

    public BaseWolf baseWolf;
    Vector2 vectorEscape;
    public float maxFleeRate = 0.5f, deviationAngle = 5, fleeLenght;
    float timeSinceFleeBegins;
    [SerializeField]
    LayerMask isEnemies;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        baseWolf.destinationSetter.target = null;
        baseWolf.destinationSetter.enabled = false;

        baseWolf.pather.maxSpeed = baseWolf.fleeSpeed; 
        baseWolf.pather.destination = FleeTarget();
        baseWolf.pather.SearchPath();
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

        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(animator.transform.position, baseWolf.viewDistance, isEnemies);
        foreach (Collider2D enemy in nearbyEnemies)
        {
            if (enemy.GetComponent<WolfRoot>())
            {
                BaseWolf enemyWolf = enemy.GetComponent<BaseWolf>();
                if (enemyWolf.baseWolfSMBState != BaseWolf.BaseWolfSMBState.FLEE) 
                {
                    enemyWolf.pack.wolves.Add(animator.GetComponent<WolfRoot>());
                    switch (enemyWolf.baseWolfSMBState)
                    {
                        case BaseWolf.BaseWolfSMBState.APPROACH:
                            animator.Play("Approach");
                            break;
                        case BaseWolf.BaseWolfSMBState.ATTACK:
                            animator.Play("Attack");
                            break;
                        case BaseWolf.BaseWolfSMBState.HARASS:
                            animator.Play("Harass");
                            break;
                        case BaseWolf.BaseWolfSMBState.OBSERVE:
                            animator.Play("Observe");
                            break;
                        case BaseWolf.BaseWolfSMBState.RUSH:
                            animator.Play("Rush");
                            break;
                        case BaseWolf.BaseWolfSMBState.WANDER:
                            animator.Play("Wander");
                            break;
                    }
                    return;
                }
            }
        }

        timeSinceFleeBegins += Time.deltaTime;
        if(timeSinceFleeBegins >= maxFleeRate)
        {
            baseWolf.pather.destination = FleeTarget();
            baseWolf.pather.SearchPath();
            timeSinceFleeBegins = 0;
        }
        if(baseWolf.ToPlayer().magnitude > baseWolf.pack.fleeDistance)
        {
            animator.Play("Wander");
        }
    }  

    Vector3 FleeTarget()
    {
        Vector3 result = Vector3.zero;
        RaycastHit2D hit2D;
        float hitLenght = fleeLenght;
        Vector3 angle = -baseWolf.ToPlayer().normalized;
        for (int i = 0; i*deviationAngle < 180; i++)
        {
            angle = -baseWolf.ToPlayer().normalized;
            angle = Quaternion.AngleAxis(deviationAngle * i, Vector3.forward) * angle;
            hit2D = Physics2D.Raycast(baseWolf.transform.position, angle, hitLenght, baseWolf.blocksLOS);
            if (hit2D.collider)
            {
                angle = -baseWolf.ToPlayer().normalized;
                angle = Quaternion.AngleAxis(-deviationAngle * i, Vector3.forward) * angle;
                hit2D = Physics2D.Raycast(baseWolf.transform.position, angle, hitLenght, baseWolf.blocksLOS);
                if (!hit2D.collider)
                {
                    result = (baseWolf.transform.position + angle * fleeLenght);
                    break;
                }
            }
            else
            {
                result = baseWolf.transform.position + angle * fleeLenght;
                break;
            }

        }
        if (result == Vector3.zero)
        {

            angle = -baseWolf.ToPlayer().normalized;
            hit2D = Physics2D.Raycast(baseWolf.transform.position, angle, hitLenght, baseWolf.blocksLOS);
            hitLenght = hit2D.distance;
            result = baseWolf.transform.position + angle * hitLenght;
        }

        return result;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        baseWolf.destinationSetter.enabled = true;
        baseWolf.pack.DetermineLeader();
    }

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