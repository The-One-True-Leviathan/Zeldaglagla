using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    public class CombatEvents
    {
        public static UnityEvent monsterWasKilled = new UnityEvent();
        public static UnityEvent monsterWasStunned;
        public static Action<HitContext> monsterWasHit;
        public static HitContext hitStunned = new HitContext(true), hitNotStunned = new HitContext(false);
        public static StunContext stunContext = new StunContext();

        public struct StunContext
        {

        }
        public struct HitContext
        {
            public bool stunned { get; set; }

            public HitContext(bool stn)
            {
                stunned = stn;
            }
        }
    }
    public struct StunStruct
    {
        public float str;
        public float lgt;

        public StunStruct(float stunStrength, float stunLength)
        {
            str = stunStrength;
            lgt = stunLength;
        }
    }

    public struct KnockBackStruct
    {
        public Vector3 ogn;
        public float str; //Strength of the knockback for knockback thresholds
        public float spd; //Speed of the knockback
        public float lgt; //Duration of the knockback, divided by kb resist

        public KnockBackStruct(Vector3 origin, float strenght, float speed, float length)
        {
            ogn = origin;
            str = strenght;
            spd = speed;
            lgt = length;
        }
    }

    public struct DamageStruct
    {
        public float dmg;
        public StunStruct stn;
        public KnockBackStruct kb;

        public DamageStruct(float damageAmount)
        {
            dmg = damageAmount;
            stn = new StunStruct(0, 0);
            kb = new KnockBackStruct(Vector3.zero, 0, 0, 0);
        }
        public DamageStruct(float damageAmount, StunStruct stun)
        {
            dmg = damageAmount;
            stn = stun;
            kb = new KnockBackStruct(Vector3.zero, 0, 0, 0);
        }

        public DamageStruct(float damageAmount, KnockBackStruct knockBack)
        {
            dmg = damageAmount;
            stn = new StunStruct(0, 0);
            kb = knockBack;
        }

        public DamageStruct(float damageAmount, StunStruct stun, KnockBackStruct knockBack)
        {
            dmg = damageAmount;
            stn = stun;
            kb = knockBack;
        }
        
    }

    public struct AttackProfile
    {
        public DamageStruct dmg;
        public float buildup, recover, hitSpan;
        public AttackProfile(DamageStruct damage, float atkBuildup, float atkRecover, float atkHitSpan = 0)
        {
            dmg = damage;
            buildup = atkBuildup;
            recover = atkRecover;
            hitSpan = atkHitSpan;
        }
    }
}
