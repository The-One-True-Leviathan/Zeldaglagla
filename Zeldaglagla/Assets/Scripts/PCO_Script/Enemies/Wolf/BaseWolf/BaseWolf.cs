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
        pack = transform.parent.GetComponent<PackManager>();
        Debug.LogWarning("Hey salut les potos je suis un loup oulala je start");
        SMB = GetComponent<Animator>();
        pather = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    override public void GoBackToState()
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
            if (player.GetComponent<HDO_CharacterCombat>())
            {
                player.GetComponent<HDO_CharacterCombat>().TakeDamage(pounceAtk.dmg, transform);
            } else
            {
                Debug.LogError("Player has no HDO_CharacterCombat");
            }
            return true;
        }
        return false;
    }

    public override void Stun(StunStruct stunTaken)
    {
        base.Stun(stunTaken);
        isInAttack = false;
        Debug.Log("oh no je suis un loup et je susi stun ouhlala");
        SMB.Play("Stunned");
    }


    public void SetAnim(string anim, Vector3 direction)
    {
        if (direction.x > 0)
        {
            animator.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction.x < 0)
        {
            animator.transform.localScale = new Vector3(1, 1, 1);
        }
        if (direction.y > 0)
        {
            anim += "_Back";
        }
        else if (direction.y < 0)
        {
            anim += "_Front";
        }
        Debug.LogWarning("Wolf :" + anim);
        animator.Play(anim);
    }
}
