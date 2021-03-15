using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using Pathfinding;

public class WolfRoot : MonsterRoot
{
    public float pounceDmg, pounceBuildup, pouceRecover, pounceLength, viewDistance = 20;
    public AttackProfile pounceAtk;
    public AIPath pather;
    public PackManager pack;
    public Animator SMB;
    public GameObject thisGameObject;

    public bool isInPounce, isPackLeader;
    // Start is called before the first frame update
    private void Awake()
    {
        thisGameObject = this.gameObject;
        SMB = GetComponent<Animator>();
        pounceAtk = new AttackProfile(new DamageStruct(pounceDmg), pounceBuildup, pouceRecover, pounceLength);
        pack = GetComponentInParent<PackManager>();
        pather = GetComponent<AIPath>();
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
