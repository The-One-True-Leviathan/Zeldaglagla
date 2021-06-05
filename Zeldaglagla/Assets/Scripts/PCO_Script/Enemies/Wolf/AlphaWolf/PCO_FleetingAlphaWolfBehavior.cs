using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PCO_FleetingAlphaWolfBehavior : PCO_AlphaWolfBehavior
{
    public override void Death()
    {
        CombatEvents.monsterWasKilled.Invoke();
        dead = true; 
        int rng = UnityEngine.Random.Range(0, drops.Count + 1);
        if (drops[rng].name == "Empty")
        {
            Instantiate(drops[rng], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
