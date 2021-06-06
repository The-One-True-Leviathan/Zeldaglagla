using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using Combat;
using Pathfinding;

namespace Monsters
{

    public abstract class MonsterRoot : MonoBehaviour
    {
        // 
        // R�sum� :
        //      Maximum health points of the monster.
        public float maxHP;
        // 
        // R�sum� :
        //      Current health points of the monster.
        public float currentHP;
        //
        // R�sum� :
        //      Stun Resist of the monster. The length of stuns is divided by this value.
        public float stunResist = 1;
        //
        // R�sum� :
        //      Stun Threshold of the monster. Stuns with a strength that is less than this value will not affect the monster.
        public float stunThreshold = 0;
        //
        // R�sum� :
        //      Accumulated Stun of the monster. Stuns with a strength below the Stun Threshold will accumulate in this value until it is higher and the monster is Stunned.
        public float accumulatedStun = 0;
        //
        // R�sum� :
        //      Stun Multiplier is multiplied with the damage taken by the monster while it is stunned.
        public float stunMultiplier = 2;
        //
        // R�sum� :
        //      Recover of the monster. The monster loses accumulated Stun at a rate of this value per second.
        public float stunRecover = 0;
        //
        // R�sum� :
        //      Knockback Resist of the monster. The length of a knockback is divided by this value.
        public float kbResist = 1;
        //
        // R�sum� :
        //      Knockback Threshold of the monster. Knockback with a strength that is less than this value will not affect the monster.
        public float kbThreshold = 0;
        public bool stunned, isInAttack, isInBuildup, isInRecover, isInHitSpan, isInKnockback, dead = false;

        public AIPath pather;
        public AIDestinationSetter destinationSetter;

        public Collider2D boundBox, attackZone;
        [NonSerialized]
        public GameObject player;
        [NonSerialized]
        public Collider2D playerCollider;
        public Animator SMB;
        public Animator animator;
        public LayerMask blocksLOS;

        public UnityEvent stunnedEvent;
        public UnityEvent stunRecoveredEvent;

        public List<GameObject> drops = new List<GameObject>();

        //public Action<CombatEvents.StunContext> stunnedEvent;
        //public Action<CombatEvents.StunContext> stunRecoveredEvent;

        public void AwakeDuPauvre()
        {
            if (stunnedEvent == null)
                stunnedEvent = new UnityEvent();
            if (stunRecoveredEvent == null)
                stunRecoveredEvent = new UnityEvent();

            player = GameObject.FindGameObjectWithTag("Player");
            playerCollider = player.GetComponent<Collider2D>();
            SMB = GetComponent<Animator>();
            pather = GetComponent<AIPath>();
            destinationSetter = GetComponent<AIDestinationSetter>();
            SMB.GetBehaviour<PCO_MonsterStunned>().baseMonster = this;

            stunRecoveredEvent.AddListener(GoBackToState);
        }

        virtual public bool Damage(DamageStruct damageTaken)
        {
            if (!stunned)
            {
                currentHP -= damageTaken.dmg;

                if (damageTaken.stn.str > stunThreshold)
                {
                    accumulatedStun = 0;
                    Stun(damageTaken.stn);
                }
                else
                {
                    accumulatedStun += damageTaken.stn.str;
                    if (accumulatedStun > stunThreshold)
                    {
                        accumulatedStun = 0;
                        Stun(damageTaken.stn);
                    } else
                    {
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

        virtual public bool Damage(DamageStruct damageTaken, bool isThisATorchAtk)
        {
            return Damage(damageTaken);
        }

        virtual public void Death()
        {
            CombatEvents.monsterWasKilled.Invoke();
            dead = true;
            GameObject deathAnim = Instantiate(transform.GetChild(0).gameObject, transform.GetChild(0).position, Quaternion.identity);
            deathAnim.transform.localScale = new Vector3(transform.GetChild(0).localScale.x * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            if (deathAnim.GetComponent<Animator>())
            {
                deathAnim.GetComponent<Animator>().Play("Die");
            }
            int rng = UnityEngine.Random.Range(0, drops.Count);
            if(drops[rng].name == "Empty")
            {
                Instantiate(drops[rng], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        virtual public void Stun(StunStruct stunTaken)
        {
            stunned = true;
            stunnedEvent.Invoke();
            stunTaken.lgt /= stunResist;
            StartCoroutine(StunCoroutine(stunTaken));
            isInAttack = isInBuildup = isInHitSpan = isInRecover = false;
        }

        IEnumerator StunCoroutine(StunStruct stunTaken)
        {
            yield return new WaitForSeconds(stunTaken.lgt);
            stunned = false;
            stunRecoveredEvent.Invoke();
        }
        public Vector3 ToPlayer()
        {
            return player.transform.position - transform.position;
        }

        virtual public void GoBackToState()
        {

        }
    }
}