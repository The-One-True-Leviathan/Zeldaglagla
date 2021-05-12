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

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        SMB.GetBehaviour<BaseShooter_SMBWander>().shooter = this;
        //SMB.GetBehaviour<BaseShooter_SMBPause>().shooter = this;
        SMB.GetBehaviour<BaseShooter_SMBApproach>().shooter = this;
        //SMB.GetBehaviour<BaseShooter_SMBAttack>().shooter = this;
        SMB.GetBehaviour<BaseShooter_SMBRelocate>().shooter = this;
        SMB.GetBehaviour<BaseShooter_SMBFlee>().shooter = this;
    }

    private void Update()
    {
    }
    public bool SightCast()
    {
        RaycastHit2D hit2D;
        Vector3 toPlayer = ToPlayer();
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


}
