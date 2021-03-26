using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Pathfinding;

public class PackManager : MonoBehaviour
{
    GameObject player;

    public List<WolfRoot> wolves;
    bool leaderIsAlive;
    bool isApproaching = false;
    public WolfRoot leader;
    HDO_HeatManager heatManager;
    [Header("Approach")]
    public float baseCircleDistance = 10, currentCircleDistance, avoidDistance = 5, circleShrinkSpeed = 0.05f, rotationSpeed = 10f;
    [Header("Observe")]
    public float observeDistance = 5, observeTime = 10, maxObserveTime = 10, minObserveTime = 3;
    float currentObserveTime;
    public bool isObserving;
    [Header("Rush")]
    public float rushCircleSize;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player.GetComponent<HDO_HeatManager>())
        {
            heatManager = player.GetComponent<HDO_HeatManager>();
        }



        foreach (WolfRoot wolf in GetComponentsInChildren<WolfRoot>())
        {
            wolves.Add(wolf);
        }
        DetermineLeader();
    }

    private void Update()
    {
        if (isObserving)
        {
            currentObserveTime += Time.deltaTime;
            if (currentObserveTime >= observeTime)
            {
                AllGoToRush();
            }
            if (player.GetComponent<HDO_HeatManager>().heatModifierPerSecond < 0 && player.GetComponent<HDO_HeatManager>().heatValue < player.GetComponent<HDO_HeatManager>().maxHeat / 2)
            {
                AllGoToRush();
            }
        }
    }

    void GoToState(string newStateName)
    {

        foreach (WolfRoot wolf in wolves)
        {
            wolf.SMB.Play(newStateName);
        }
    }

    public void DetermineLeader()
    {
        leaderIsAlive = false;
        int rng = Random.Range(0, wolves.Count);
        wolves[rng].isPackLeader = true;
        leader = wolves[rng];
    }

    public void AllGoToApproach()
    {
        if (!isApproaching)
        {
            isApproaching = true;

        }
    }

    public void StartCircling()
    {
        GoToState("Approach");
    }

    public void AllGoToObserve()
    {
        isObserving = true;
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<HDO_HeatManager>().heatModifierPerSecond > 0)
        {
            observeTime = minObserveTime;
        } else
        {
            observeTime = maxObserveTime;
        }
        GoToState("Observe");
    }

    public void AllGoToRush()
    {
        isObserving = false;
        GoToState("Rush");
    }
}
