using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooterBehavior : ShooterRoot
{
    public enum BaseShooterSMBState { WANDER, PAUSE, APPROACH, ATTACK, RELOCATE, FLEE };
    public BaseShooterSMBState baseShooterSMBState;

    public float walkSpeed, runSpeed, patrolWaitTime, pauseTime;
    public bool cyclicPatrol;
    public List<Transform> patrolTargets = new List<Transform>();
    int animationIndex; //0 = Idle, 1 = Idle up, 2 = Walk, 3 = Walk up, 4 = Buildup, 5 = Buildup up, 6 = shoot, 7 = shoot up;
    bool isUp, isRight;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        AwakeDuPauvre();
        SMB.GetBehaviour<BaseShooter_SMBWander>().shooter = this;
        SMB.GetBehaviour<BaseShooter_SMBPause>().shooter = this;
        SMB.GetBehaviour<BaseShooter_SMBApproach>().shooter = this;
        SMB.GetBehaviour<BaseShooter_SMBAttack>().shooter = this;
        SMB.GetBehaviour<BaseShooter_SMBRelocate>().shooter = this;
        SMB.GetBehaviour<BaseShooter_SMBFlee>().shooter = this;
    }

    private void Update()
    {
        AnimationManager();
    }
    public bool SightCast()
    {
        RaycastHit2D hit2D;
        Vector3 toPlayer = ToPlayer().normalized;
        //RaycastHit hit;
        float hitLength = maxSight;
        hit2D = Physics2D.Raycast(transform.position, toPlayer, hitLength, blocksLOS);
        //Physics.Raycast(transform.position, toPlayer, out hit, hitLength, blocksLOS);
        if (hit2D.collider)
        {
            hitLength = hit2D.distance;
        }



        hit2D = Physics2D.Raycast(transform.position, toPlayer, hitLength, isPlayer);
        if (hit2D.collider)
        {
            playerInSight = true;
            hitLength = hit2D.distance;
            playerInMemory = true;
            timeSinceLastSeen = 0;
        }
        else
        {
            playerInSight = false;
        }

        Debug.DrawRay(transform.position, toPlayer * hitLength, Color.red);

        return playerInSight;

    }

    public void SetAnim(int anim)
    {
        animationIndex = anim;
    }
    void AnimationManager()
    {
        switch (baseShooterSMBState)
        {
            case BaseShooterSMBState.WANDER:
                AnimateWithWalkOrientation();
                animationIndex = 1;
                break;
            case BaseShooterSMBState.APPROACH:
                AnimateWithWalkOrientation();
                animationIndex = 1;
                break;
            case BaseShooterSMBState.FLEE:
                AnimateWithWalkOrientation();
                animationIndex = 1;
                break;
            case BaseShooterSMBState.RELOCATE:
                AnimateWithWalkOrientation();
                animationIndex = 1;
                break;
        }
        if (isRight) animator.transform.localScale = new Vector3(-1, 1, 1);
        if (isUp && animationIndex != -1) animationIndex += 1;

        switch (animationIndex)
        {
            case 0:
                animator.Play("PCO_Range_Idle_Front");
                break;
            case 1:
                animator.Play("PCO_Range_Idle_Back");
                break;
            case 2:
                animator.Play("PCO_Range_Walk_Front");
                break;
            case 3:
                animator.Play("PCO_Range_Walk_Back");
                break;
            case 4:
                animator.Play("PCO_Range_Buildup_Front");
                break;
            case 5:
                animator.Play("PCO_Range_Buildup_Back");
                break;
            case 6:
                animator.Play("PCO_Range_Shoot_Front");
                break;
            case 7:
                animator.Play("PCO_Range_Shoot_Back");
                break;
        }
    }

    void AnimateWithWalkOrientation()
    {
        if (pather.desiredVelocity.x > 0)
        {
            isRight = true;
        } else if (pather.desiredVelocity.x < 0)
        {
            isRight = false;
        }
        if (pather.desiredVelocity.y > 0)
        {
            isUp = true;
        } else if (pather.desiredVelocity.y < 0)
        {
            isUp = false;
        }
        if (pather.desiredVelocity.magnitude == 0) animationIndex = 0;
    }

}
