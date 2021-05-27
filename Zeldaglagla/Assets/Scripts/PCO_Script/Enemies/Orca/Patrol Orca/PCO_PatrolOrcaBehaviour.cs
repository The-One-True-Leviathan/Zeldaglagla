using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using Pathfinding;

public class PCO_PatrolOrcaBehaviour : PCO_BaseOrcaBehaviour
{
    public List<Transform> patrolPoints;
    public bool cyclicPatrol;
    public float patrolWaitTime;
    [Header("SightCast")]
    public bool playerInSight;
    public bool playerInMemory;
    float timeSinceLastSeen;

    // Start is called before the first frame update
    void Start()
    {
        circleCenterBehaviour = circleCenter.GetComponent<PCO_OrcaCircleCenterBehaviour>();
        circleCenterBehaviour.AssignOrca(transform, this);
        AwakeDuPauvre();
        SMB.GetBehaviour<PCO_PatrolOrca_SMB_Wander>().baseOrca = this;
        SMB.GetBehaviour<PCO_BaseOrca_SMB_Circle>().baseOrca = this;
        SMB.GetBehaviour<PCO_BaseOrca_SMB_Approach>().baseOrca = this;
        SMB.GetBehaviour<PCO_BaseOrca_SMB_Attack>().baseOrca = this;

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
