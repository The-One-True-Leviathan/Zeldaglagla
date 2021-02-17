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

    public bool isInPounce;
    // Start is called before the first frame update
    private void Awake()
    {
        pounceAtk = new AttackProfile(new DamageStruct(pounceDmg), pounceBuildup, pouceRecover, pounceLength);
    }
    void Start()
    {
        pather = GetComponent<AIPath>();
    }
}
