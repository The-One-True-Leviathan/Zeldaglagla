using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Pathfinding;

public class PackManager : MonoBehaviour
{
    public List<WolfRoot> wolves;
    bool leaderIsAlive;
    public WolfRoot leader;

    public void DetermineLeader()
    {
        leaderIsAlive = false;
        foreach(WolfRoot wolf in wolves)
        {
            if (wolf.isPackLeader)
            {
                leaderIsAlive = true;
                return;
            }
        }
        if (!leaderIsAlive)
        {
            int rng = Random.Range(1, wolves.Count);
            wolves[rng].isPackLeader = true;
        }

    }
}
