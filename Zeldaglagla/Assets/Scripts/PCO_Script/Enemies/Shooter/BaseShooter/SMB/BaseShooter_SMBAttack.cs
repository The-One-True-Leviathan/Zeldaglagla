using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseShooter_SMBAttack : StateMachineBehaviour
{
    enum CmbtState { BUILDUP, HITSPAN, RECOVER };
    public BaseShooterBehavior shooter;
    public float buildupTime, recoverTime, timeBetweenShots;
    public int numberOfShots;
    float buildup, recover, timeSinceLastShot;
    int shotsFired;
    bool shotsLeft;
    CmbtState cmbtState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        buildup = recover = 0;
        shooter.baseShooterSMBState = BaseShooterBehavior.BaseShooterSMBState.ATTACK;
        shooter.pather.maxSpeed = shooter.walkSpeed;
        cmbtState = CmbtState.BUILDUP;
        shooter.pather.enabled = false;
        shooter.destinationSetter.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (cmbtState)
        {
            case CmbtState.BUILDUP:
                buildup += Time.deltaTime;
                if (buildup >= buildupTime)
                {
                    cmbtState = CmbtState.HITSPAN;
                    shotsFired = 0; timeSinceLastShot = 0; shotsLeft = false;
                    Shoot();
                }
                break;
            case CmbtState.HITSPAN:
                if (numberOfShots - shotsFired >= 1)
                {
                    timeSinceLastShot += Time.deltaTime;
                    if (timeSinceLastShot >= timeBetweenShots)
                    {
                        timeSinceLastShot = 0f;
                        Shoot();
                    }
                }
                    else 
                {
                    cmbtState = CmbtState.RECOVER;
                    recover = 0;
                }
                break;
            case CmbtState.RECOVER:
                shooter.pather.maxSpeed = shooter.walkSpeed;
                recover += Time.deltaTime;
                if (recover >= recoverTime)
                {
                    shooter.pather.enabled = true;
                    shooter.destinationSetter.enabled = true;
                    animator.Play("Relocate");
                }
                break;
        }
        
    }

    void Shoot()
    {
        shotsFired++;
        Instantiate(shooter.projectile, shooter.transform.position, Quaternion.LookRotation(Vector3.forward, shooter.ToPlayer().normalized));
        Debug.LogWarning("Shot fired !");
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
