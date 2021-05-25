using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolWolf : BaseWolf
{
    public List<Transform> patrolTargets;
    public bool cyclicPatrol;
    public float patrolWaitTime;
    [Header("SightCast")]
    public bool playerInSight;
    public bool playerInMemory;
    public LayerMask isPlayer;
    float timeSinceLastSeen;



    // Start is called before the first frame update
    void Start()
    {
        SMB.GetBehaviour<PatrolWolfSMBWander>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBApproach>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBObserve>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBRush>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBHarass>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBAttack>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBFlee>().baseWolf = this;
    }

    public bool SightCast()
    {
        RaycastHit2D hit2D;
        Vector3 toPlayer = ToPlayer().normalized;
        //RaycastHit hit;
        float hitLength = viewDistance;
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
