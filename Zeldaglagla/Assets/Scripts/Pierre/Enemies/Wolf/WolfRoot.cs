using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using Pathfinding;

public class WolfRoot : MonsterRoot
{
    public float pounceDmg, pounceBuildup, pouceRecover, pounceLength;
    public AttackProfile pounceAtk;
    public AIPath pather;
    public PackManager pack;

    public bool isInPounce, isPackLeader;
    // Start is called before the first frame update
    private void Awake()
    {
        pounceAtk = new AttackProfile(new DamageStruct(pounceDmg), pounceBuildup, pouceRecover, pounceLength);
        pack = GetComponentInParent<PackManager>();
        pather = GetComponent<AIPath>();
    }
    void Start()
    {
    }
}
