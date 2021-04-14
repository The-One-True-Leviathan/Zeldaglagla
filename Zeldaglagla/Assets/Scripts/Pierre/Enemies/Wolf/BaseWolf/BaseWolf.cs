using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseWolf : WolfRoot
{
    public enum BaseWolfSMBState { WANDER, APPROACH, OBSERVE, RUSH, ATTACK, HARASS, FLEE };
    public BaseWolfSMBState baseWolfSMBState;

    public float wanderSpeed, approachSpeed, observeSpeed, rushSpeed, pounceSpeed, fleeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        SMB.GetBehaviour<BaseWolfSMBWander>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBApproach>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBObserve>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBRush>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBHarass>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBAttack>().baseWolf = this;
        SMB.GetBehaviour<BaseWolfSMBFlee>().baseWolf = this;


        stunRecoveredEvent += ctx => GoBackToState(); 
    }

    void GoBackToState()
    {
        switch (baseWolfSMBState)
        {
            case BaseWolfSMBState.WANDER:
                SMB.Play("Wander");
                break;
            case BaseWolfSMBState.APPROACH:
                SMB.Play("Approach");
                break;
            case BaseWolfSMBState.OBSERVE:
                SMB.Play("Observe");
                break;
            case BaseWolfSMBState.RUSH:
                SMB.Play("Rush");
                break;
            case BaseWolfSMBState.ATTACK:
                SMB.Play("Harass");
                break;
            case BaseWolfSMBState.HARASS:
                SMB.Play("Harass");
                break;
            case BaseWolfSMBState.FLEE:
                SMB.Play("Flee");
                break;
        }
    }

    public bool Attack()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackDistance)
        {
            //damagePlayer
            return true;
        }
        return false;
    }

    public override void Stun(StunStruct stunTaken)
    {
        base.Stun(stunTaken);
        isInAttack = false;
        SMB.Play("Stunned");
    }

    
}
