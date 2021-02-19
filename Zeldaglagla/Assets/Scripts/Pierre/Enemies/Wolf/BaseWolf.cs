using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseWolf : WolfRoot
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().GetBehaviour<BaseWolfSMBWander>().baseWolf = this;
    }

    private void Update()
    {
        if (isInHitSpan)
        {
            HitSpanResolve();
        }
    }

    void Approach()
    {
        //pather.destination = player.transform.position
    }

    void Attack()
    {

    }

    void Pounce()
    {
        isInAttack = true;
        StartCoroutine(PounceBuildupCoroutine());
    }

    IEnumerator PounceBuildupCoroutine()
    {
        isInBuildup = true;
        yield return new WaitForSeconds(pounceAtk.buildup);
        isInBuildup = false;
        isInHitSpan = true;
        StartCoroutine(PounceHitSpanCoroutine());
    }

    void HitSpanResolve()
    {
        if (attackZone.IsTouching(playerCollider))
        {
            /*
             * bool parried = player.Damage(pounceAtk.dmg)
             * if (parried)
             * {
             *  Stun(player.parryStun);
             * }
             * 
             */
        }
    }

    IEnumerator PounceHitSpanCoroutine()
    {
        isInHitSpan = true;
        isInPounce = true;
        yield return new WaitForSeconds(pounceAtk.hitSpan);
        isInHitSpan = false;
        isInPounce = false;
        StartCoroutine(PounceRecoverCoroutine());
    }

    IEnumerator PounceRecoverCoroutine()
    {
        isInRecover = true;
        yield return new WaitForSeconds(pounceAtk.recover);
        isInRecover = false;
        isInAttack = false;
    }

    public override void Stun(StunStruct stunTaken)
    {
        base.Stun(stunTaken);
        isInPounce = false;
    }
}
