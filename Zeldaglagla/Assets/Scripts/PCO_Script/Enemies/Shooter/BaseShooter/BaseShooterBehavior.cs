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
    int animationIndex; //0 = Idle, 1 = Idle up, 2 = Walk, 3 = Walk up, 4 = Buildup, 5 = Buildup up, 6 = Shoot, 7 = Shoot up, 8 = Recover, 9 = Recover up
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

    override public void GoBackToState()
    {
        switch (baseShooterSMBState)
        {
            case BaseShooterSMBState.WANDER:
                SMB.Play("Flee");
                break;
            case BaseShooterSMBState.APPROACH:
                SMB.Play("Flee");
                break;
            case BaseShooterSMBState.PAUSE:
                SMB.Play("Flee");
                break;
            case BaseShooterSMBState.RELOCATE:
                SMB.Play("Flee");
                break;
            case BaseShooterSMBState.ATTACK:
                SMB.Play("Flee");
                break;
            case BaseShooterSMBState.FLEE:
                SMB.Play("Flee");
                break;
        }
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

    public void SetAnim(int anim, Vector3 direction)
    {
        animationIndex = anim;
        if (direction.x > 0)
        {
            isRight = true;
        }
        else if (direction.x < 0)
        {
            isRight = false;
        }
        if (direction.y > 0)
        {
            isUp = true;
            if (animationIndex != -1) animationIndex += 1;
        }
        else if (direction.y < 0)
        {
            isUp = false;
        }
    }
    void AnimationManager()
    {
        switch (baseShooterSMBState)
        {
            case BaseShooterSMBState.WANDER:
                if (!AnimateWithWalkOrientation()) animationIndex = 0;
                break;
            case BaseShooterSMBState.APPROACH:
                if (!AnimateWithWalkOrientation()) animationIndex = 0;
                break;
            case BaseShooterSMBState.FLEE:
                if (!AnimateWithWalkOrientation()) animationIndex = 0;
                break;
            case BaseShooterSMBState.RELOCATE:
                if (!AnimateWithWalkOrientation()) animationIndex = 0;
                break;
            case BaseShooterSMBState.PAUSE:
                animationIndex = 0;
                break;
        }
        if (isRight) animator.transform.localScale = new Vector3(-1, 1, 1); else animator.transform.localScale = new Vector3(1, 1, 1);
        //if (isUp && animationIndex != -1) animationIndex += 1;

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
            case 8:
                animator.Play("PCO_Range_Recover_Front");
                break;
            case 9:
                animator.Play("PCO_Range_Recover_Back");
                break;
        }
    }

    bool AnimateWithWalkOrientation()
    {
        if (pather.velocity.x > 0.12)
        {
            isRight = true;
        } else if (pather.velocity.x < -0.12)
        {
            isRight = false;
        }
        if (pather.velocity.y > 0)
        {
            isUp = true;
            animationIndex = 3;
        } else if (pather.velocity.y < 0)
        {
            isUp = false;
            animationIndex = 2;
        }
        if (pather.velocity.magnitude < 0.05) return false;
        Debug.Log("Walk with velocity, index = " + animationIndex);
        return true;
    }
    void ShooterShot()
    {
        FindObjectOfType<AudioManager>().Play("ShooterShot");
    }

}
