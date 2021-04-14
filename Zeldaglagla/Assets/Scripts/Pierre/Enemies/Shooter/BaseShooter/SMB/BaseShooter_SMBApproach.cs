using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooter_SMBApproach : StateMachineBehaviour
{
    public BaseShooterBehavior shooter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shooter.baseShooterSMBState = BaseShooterBehavior.BaseShooterSMBState.APPROACH;
        shooter.destinationSetter.target = shooter.player.transform;
        shooter.destinationSetter.enabled = true;

        shooter.pather.maxSpeed = shooter.runSpeed;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (shooter.SightCast())
        {
            if (shooter.ToPlayer().magnitude < shooter.minAtkDistance)
            {
                animator.Play("Flee");
            }
            else if (shooter.ToPlayer().magnitude < shooter.maxAtkDistance)
            {
                animator.Play("Attack");
            }
        }

        if (!shooter.playerInMemory)
        {
            animator.Play("Pause");
        }
    }
}
