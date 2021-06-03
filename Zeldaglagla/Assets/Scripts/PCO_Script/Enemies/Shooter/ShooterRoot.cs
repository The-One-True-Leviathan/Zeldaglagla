using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using Pathfinding;

public class ShooterRoot : MonsterRoot
{
    public LayerMask isPlayer;

    public float shotDmg, shotBuildup, shotRecover, shotKBStrength, shotKBSpeed, shotKBTime, shotSpeed, minAtkDistance, maxAtkDistance;

    public int shotNumber = 1;
    public float maxSight, timeSinceLastSeen;
    public bool playerInSight, playerInMemory;

    private void Awake()
    {
        pather = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }
}
