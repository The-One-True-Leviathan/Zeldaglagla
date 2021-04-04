using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseWolf : WolfRoot
{
    public enum BaseWolfSMBState { WANDER, APPROACH, OBSERVE, RUSH, ATTACK, HARASS, FLEE };
    public BaseWolfSMBState baseWolfSMBState;

    public float wanderSpeed, approachSpeed, observeSpeed, rushSpeed, pounceSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().GetBehaviour<BaseWolfSMBWander>().baseWolf = this;
        GetComponent<Animator>().GetBehaviour<BaseWolfSMBApproach>().baseWolf = this;
        GetComponent<Animator>().GetBehaviour<BaseWolfSMBObserve>().baseWolf = this;
        GetComponent<Animator>().GetBehaviour<BaseWolfSMBRush>().baseWolf = this;
        GetComponent<Animator>().GetBehaviour<BaseWolfSMBHarass>().baseWolf = this;
        GetComponent<Animator>().GetBehaviour<BaseWolfSMBAttack>().baseWolf = this;


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
