using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using Pathfinding;

public class ShooterRoot : MonsterRoot
{

    public AIPath pather;
    public AIDestinationSetter destinationSetter;
    public LayerMask isPlayer;

    public float shotDmg, shotBuildup, shotRecover, shotKB, minAtkDistance, maxAtkDistance;
    public float maxSight, timeSinceLastSeen;
    public bool playerInSight, playerInMemory;

    private void Awake()
    {
        pather = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }
}
