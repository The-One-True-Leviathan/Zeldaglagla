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
        numberOfShots = shooter.shotNumber;
        buildup = recover = timeSinceLastShot = 0;
        shotsLeft = true;
        shooter.baseShooterSMBState = BaseShooterBehavior.BaseShooterSMBState.ATTACK;
        shooter.pather.maxSpeed = shooter.walkSpeed;
        cmbtState = CmbtState.BUILDUP;
        shooter.pather.enabled = false;
        shotsFired = 0;
        shooter.destinationSetter.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (cmbtState)
        {
            case CmbtState.BUILDUP:
                shooter.SetAnim(4, shooter.ToPlayer());
                buildup += Time.deltaTime;
                if (buildup >= buildupTime)
                {
                    if (shotsLeft) Shoot();
                    shotsLeft = false;
                    shooter.SetAnim(6, shooter.ToPlayer());
                    timeSinceLastShot += Time.deltaTime;
                    if (timeSinceLastShot >= timeBetweenShots)
                    {
                        cmbtState = CmbtState.HITSPAN;
                    }
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
                        shooter.SetAnim(6, shooter.ToPlayer());
                        shotsLeft = true;
                    }
                }
                else
                {
                    if (shotsLeft)
                    {
                        timeSinceLastShot += Time.deltaTime;
                        if (timeSinceLastShot >= timeBetweenShots)
                        {
                            cmbtState = CmbtState.RECOVER;
                            recover = 0;
                            shooter.SetAnim(8, shooter.ToPlayer());
                        }
                    } else
                    {
                        cmbtState = CmbtState.RECOVER;
                        recover = 0;
                        shooter.SetAnim(8, shooter.ToPlayer());
                    }
                }
                break;
            case CmbtState.RECOVER:
                shooter.pather.maxSpeed = shooter.walkSpeed;
                recover += Time.deltaTime;
                if (recover >= recoverTime)
                {
                    shooter.pather.enabled = true;
                    shooter.destinationSetter.enabled = true;
                    Debug.Log("Shooter Relocating");
                    animator.Play("Relocate");
                }
                break;
        }
        
    }

    void Shoot()
    {
        //FindObjectOfType<AudioManager>().Play("ShooterShot");

        shotsFired++;
        Projectile_Behaviour projectile;
        projectile = Instantiate(shooter.projectile, shooter.transform.position, Quaternion.LookRotation(Vector3.forward, shooter.ToPlayer().normalized)).GetComponent<Projectile_Behaviour>();
        projectile.direction = shooter.ToPlayer().normalized;
        projectile.damage = shooter.shotDmg;
        projectile.knockBackStrength = shooter.shotKBStrength;
        projectile.knockBackSpeed = shooter.shotKBSpeed;
        projectile.knockBackTime = shooter.shotKBTime;
        projectile.speed = shooter.shotSpeed;
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
