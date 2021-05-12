using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooter_SMBFlee : StateMachineBehaviour
{

    public BaseShooterBehavior shooter;
    public float maxFleeRate = 0.5f, deviationAngle = 5, fleeLenght;
    float timeSinceFleeBegins;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shooter.baseShooterSMBState = BaseShooterBehavior.BaseShooterSMBState.FLEE;
        shooter.destinationSetter.target = null;
        shooter.destinationSetter.enabled = false;

        shooter.pather.maxSpeed = shooter.runSpeed;
        shooter.pather.destination = FleeTarget();
        shooter.pather.SearchPath();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSinceFleeBegins += Time.deltaTime;
        if (timeSinceFleeBegins >= maxFleeRate)
        {
            Vector3 destination = FleeTarget();
            if (destination == Vector3.zero)
            {
                animator.Play("Attack");
            }
            shooter.pather.destination = destination;
            shooter.pather.SearchPath();
            timeSinceFleeBegins = 0;
        }
        if (shooter.ToPlayer().magnitude > shooter.minAtkDistance)
        {
            animator.Play("Attack");
        }
    }

    Vector3 FleeTarget()
    {
        Vector3 result = Vector3.zero;
        RaycastHit2D hit2D;
        float hitLenght = fleeLenght;
        Vector3 angle = -shooter.ToPlayer().normalized;
        for (int i = 0; i * deviationAngle < 180; i++)
        {
            angle = -shooter.ToPlayer().normalized;
            angle = Quaternion.AngleAxis(deviationAngle * i, Vector3.forward) * angle;
            hit2D = Physics2D.Raycast(shooter.transform.position, angle, hitLenght, shooter.blocksLOS);
            if (hit2D.collider)
            {
                angle = -shooter.ToPlayer().normalized;
                angle = Quaternion.AngleAxis(-deviationAngle * i, Vector3.forward) * angle;
                hit2D = Physics2D.Raycast(shooter.transform.position, angle, hitLenght, shooter.blocksLOS);
                if (!hit2D.collider)
                {
                    result = (shooter.transform.position + angle * fleeLenght);
                    break;
                }
            }
            else
            {
                result = shooter.transform.position + angle * fleeLenght;
                break;
            }

        }

        return result;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shooter.destinationSetter.enabled = true;
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
