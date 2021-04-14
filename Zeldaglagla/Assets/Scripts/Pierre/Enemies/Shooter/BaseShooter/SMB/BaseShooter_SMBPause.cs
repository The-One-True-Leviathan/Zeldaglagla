using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooter_SMBPause : StateMachineBehaviour
{
    public BaseShooterBehavior shooter;
    float timeSincePause;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shooter.baseShooterSMBState = BaseShooterBehavior.BaseShooterSMBState.PAUSE;
        shooter.destinationSetter.target = null;
        shooter.destinationSetter.enabled = false;
        shooter.pather.maxSpeed = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeSincePause += Time.deltaTime;
        if (timeSincePause >= shooter.pauseTime)
        {
            if (!shooter.playerInMemory)
            {
                animator.Play("Wander");
            }
            else
            {
                animator.Play("Approach");
            }
            timeSincePause = 0;
        }
    }
}
