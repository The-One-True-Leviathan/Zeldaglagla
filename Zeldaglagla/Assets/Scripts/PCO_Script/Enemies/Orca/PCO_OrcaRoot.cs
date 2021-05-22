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

    public float atkDmg, atkBuildup, atkRecover, atkKBStrength, atkKBSpeed, atkKBTime;

    private void Awake()
    {
        pather = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }
}
