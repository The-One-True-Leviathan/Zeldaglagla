using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Pathfinding;
using Combat;

public class PCO_OrcaRoot : MonsterRoot
{
    public AIPath pather;
    public AIDestinationSetter destinationSetter;
    public LayerMask isPlayer;

    public float atkDmg, atkBuildup, atkHitspan, atkRecover, atkKBStrength, atkKBSpeed, atkKBTime, atkRange, atkStunStr, atkStunTime;
    [System.NonSerialized]
    public DamageStruct orcaDamage;

    private void Awake()
    {
        pather = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        StunStruct stun = new StunStruct(atkStunStr, atkStunTime);
        KnockBackStruct knockBack = new KnockBackStruct(transform.position, atkKBStrength, atkKBSpeed, atkKBTime);
        orcaDamage = new DamageStruct(atkDmg, stun, knockBack);
    }

    public bool Attack()
    {
        if (ToPlayer().magnitude < atkRange)
        {
            if (player.GetComponent<HDO_CharacterCombat>())
            {
                player.GetComponent<HDO_CharacterCombat>().TakeDamage(orcaDamage, transform);
            }
            else
            {
                Debug.LogError("Player has no HDO_CharacterCombat");
            }
            return true;
        }
        return false;
    }
}
