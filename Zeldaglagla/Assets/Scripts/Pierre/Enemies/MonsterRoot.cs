using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using Combat;

namespace Monsters
{
    enum monsterStates { IDLE, APPROACH, ATTACK, HARASS, FLEE, OBSERVE };

    public abstract class MonsterRoot : MonoBehaviour
    {
        // 
        // Résumé :
        //      Maximum health points of the monster.
        public float maxHP;
        // 
        // Résumé :
        //      Current health points of the monster.
        public float currentHP;
        //
        // Résumé :
        //      Stun Resist of the monster. The length of stuns is divided by this value.
        public float stunResist = 1;
        //
        // Résumé :
        //      Stun Threshold of the monster. Stuns with a strength that is less than this value will not affect the monster.
        public float stunThreshold = 0;
        //
        // Résumé :
        //      Accumulated Stun of the monster. Stuns with a strength below the Stun Threshold will accumulate in this value until it is higher and the monster is Stunned.
        public float accumulatedStun = 0;
        //
        // Résumé :
        //      Stun Multiplier is multiplied with the damage taken by the monster while it is stunned.
        public float stunMultiplier = 2;
        //
        // Résumé :
        //      Recover of the monster. The monster loses accumulated Stun at a rate of this value per second.
        public float stunRecover = 0;
        //
        // Résumé :
        //      Knockback Resist of the monster. The length of a knockback is divided by this value.
        public float kbResist = 1;
        //
        // Résumé :
        //      Knockback Threshold of the monster. Knockback with a strength that is less than this value will not affect the monster.
        public float kbThreshold = 0;
        monsterStates monsterState;
        bool stunned, isInAttack, isInBuildup, isInRecover, isInKnockback, dead = false;

        public Action<CombatEvents.StunContext> stunnedEvent;
        public Action<CombatEvents.StunContext> stunRecoveredEvent;

        virtual public bool Damage(DamageStruct damageTaken)
        {
            if (!stunned)
            {
                currentHP -= damageTaken.dmg;

                if (damageTaken.stn.str > stunThreshold)
                {
                    CombatEvents.monsterWasHit.Invoke(CombatEvents.hitStunned);
                    accumulatedStun = 0;
                    Stun(damageTaken.stn);
                }
                else
                {
                    accumulatedStun += damageTaken.stn.str;
                    if (accumulatedStun > stunThreshold)
                    {
                        CombatEvents.monsterWasHit.Invoke(CombatEvents.hitStunned);
                        accumulatedStun = 0;
                        Stun(damageTaken.stn);
                    } else
                    {
                        CombatEvents.monsterWasHit.Invoke(CombatEvents.hitNotStunned);
                    }
                }
            }
            else
            {
                currentHP -= stunMultiplier * damageTaken.dmg;
            }



            if (currentHP <= 0)
            {
                Death();
                return true;
            } else
            {
                return false;
            }
        }

        virtual public void Death()
        {
            CombatEvents.monsterWasKilled.Invoke();
            dead = true;
        }

        virtual public void Stun(StunStruct stunTaken)
        {
            stunned = true;
            stunnedEvent.Invoke(CombatEvents.stunContext);
            CombatEvents.monsterWasStunned.Invoke();
            stunTaken.lgt /= stunResist;
            StartCoroutine(StunCoroutine(stunTaken));
        }

        IEnumerator StunCoroutine(StunStruct stunTaken)
        {
            yield return new WaitForSeconds(stunTaken.lgt);
            stunned = false;
            stunRecoveredEvent.Invoke(CombatEvents.stunContext);
        }

    }
}