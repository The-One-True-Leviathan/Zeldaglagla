using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;

public class HeavyMonster : MonsterRoot
{
    private void Start()
    {
        stunnedEvent.AddListener(DoSomethingWhenStunned);
        CombatEvents.monsterWasHit += ctx => { Debug.Log("hit!"); };
    }

    void DoSomethingWhenStunned()
    {

    }

    override public bool Damage(DamageStruct damage)
    {
        return true;
    }
}
