using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PCO_FleetingAlphaWolfBehavior : PCO_AlphaWolfBehavior
{
    public override void Death()
    {
        pack.wolves.Remove(this);
        pack.currentWolves--;
            pack.AllGoToFlee();
            foreach (WolfRoot wolf in pack.wolves)
            {
                wolf.defeated = true;
            }
        CombatEvents.monsterWasKilled.Invoke();
        dead = true; 
        int rng = UnityEngine.Random.Range(0, drops.Count);
            Instantiate(drops[0], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
