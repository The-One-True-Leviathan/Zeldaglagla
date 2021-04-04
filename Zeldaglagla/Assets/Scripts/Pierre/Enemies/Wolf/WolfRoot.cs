using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using Pathfinding;

public class WolfRoot : MonsterRoot 
{
    public float pounceDmg, pounceBuildup, pounceHitSpan, pouceRecover, pounceLength, viewDistance = 20, attackDistance;
    public AttackProfile pounceAtk;
    public AIPath pather;
    public PackManager pack;
    public Animator SMB;
    public GameObject thisGameObject;
    public bool hasAttacked;

    public bool isPackLeader;
    // Start is called before the first frame update
    private void Awake()
    {
        thisGameObject = this.gameObject;
        SMB = GetComponent<Animator>();
        pounceAtk = new AttackProfile(new DamageStruct(pounceDmg), pounceBuildup, pouceRecover, pounceHitSpan);
        pack = GetComponentInParent<PackManager>();
        pather = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void GoToApproach()
    {
        Debug.LogWarning("Moving to Approach"); 
        GetComponent<AIDestinationSetter>().enabled = true;
        if (isPackLeader)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPackCircle>().CreateCircle(pack);
        }
    }
}
