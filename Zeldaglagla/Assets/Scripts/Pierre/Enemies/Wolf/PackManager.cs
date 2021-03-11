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

    private void Start()
    {
        foreach (WolfRoot wolf in GetComponentsInChildren<WolfRoot>())
        {
            wolves.Add(wolf);
        }
        DetermineLeader();
    }

    public void DetermineLeader()
    {
        leaderIsAlive = false;
        int rng = Random.Range(0, wolves.Count);
        wolves[rng].isPackLeader = true;
        leader = wolves[rng];
    }
}
