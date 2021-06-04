using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Pathfinding;

public class PackManager : MonoBehaviour
{
    GameObject player, heatManagerGO;

    public List<WolfRoot> wolves;
    public PlayerPackCircle packCircle;
    public int startingWolves, currentWolves;
    bool leaderIsAlive;
    bool isApproaching = false;
    public bool isInAttack = false;
    public WolfRoot wolfInAttack;
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
    [Header("Flee")]
    public float fleeDistance = 20;
    private void Awake()
    {
        CountWolves(true);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        heatManagerGO = GameObject.FindGameObjectWithTag("HeatManager");
        packCircle = player.GetComponent<PlayerPackCircle>();

        if (heatManagerGO.GetComponent<HDO_HeatManager>())
        {
            heatManager = heatManagerGO.GetComponent<HDO_HeatManager>();
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
            if (heatManager.heatModifierPerSecond < 0 && heatManager.heatValue < heatManager.maxHeat / 2)
            {
                AllGoToRush();
            }
        }
    }

    public void StartHarassing()
    {
        packCircle.SetCircleSize(rushCircleSize);
        GoToState("Harass");
    }

    public void SetNextAttack(float time = 0)
    {
        if (time == 0)
        {

            int rng = Random.Range(0, wolves.Count);
            if (wolfInAttack != wolves[rng] || wolves.Count == 1)
            {
                wolfInAttack = wolves[rng];
            }
            else
            {
                if (rng + 1 < wolves.Count)
                {
                    wolfInAttack = wolves[rng + 1];
                }
                else
                {
                    wolfInAttack = wolves[rng - 1];
                }
            }
            Debug.LogWarning("Wolf Sent in Attack");
            wolfInAttack.SMB.Play("Attack");
        } else
        {
            StartCoroutine(NextAttackCoroutine(time));
        }
    }

    IEnumerator NextAttackCoroutine(float time)
    {
        Debug.LogWarning("Next Attack Waiting");
        yield return new WaitForSeconds(time);
        Debug.LogWarning("Next Attack Launched");
        SetNextAttack();
    }

    void GoToState(string newStateName)
    {

        foreach (WolfRoot wolf in wolves)
        {
            wolf.SMB.Play(newStateName);
        }
    }

    public int CountWolves(bool resetStartingNumber = false)
    {
        wolves.Clear();

        foreach (WolfRoot wolf in GetComponentsInChildren<WolfRoot>())
        {
            wolves.Add(wolf);
        }
        if (resetStartingNumber)
        {
            startingWolves = wolves.Count;
            return currentWolves = startingWolves;
        } else
        {
            return currentWolves;
        }
    }

    public void DetermineLeader()
    {
        bool alphaIsInPack = false;
        int alphaID = 0;
        if (!leaderIsAlive)
        {
            foreach (WolfRoot wolf in wolves)
            {
                wolf.isPackLeader = false;
                if (wolf.GetComponent<PCO_AlphaWolfBehavior>())
                {
                    alphaIsInPack = true;
                    return;
                } else
                {
                    alphaID++;
                }
            }
            int rng = Random.Range(0, wolves.Count);
            wolves[rng].isPackLeader = true;
            leader = wolves[rng];
            if (alphaIsInPack)
            {
                leader = wolves[alphaID];
            }
            leaderIsAlive = true;
        }
    }

    public void AllGoToApproach()
    {
        isObserving = false;
        if (!isApproaching)
        {
            isApproaching = true;
        }
        foreach (WolfRoot wolf in wolves)
        {
            wolf.GoToApproach();
        }
    }

    public void StartCircling()
    {
        GoToState("Approach");
    }

    public void AllGoToObserve()
    {
        isApproaching = false;
        isObserving = true;
        if (heatManager.heatModifierPerSecond > 0)
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
        isApproaching = false;
        isObserving = false;
        GoToState("Rush");
    }

    public void AllGoToFlee()
    {
        isApproaching = false;
        isObserving = false;
        GoToState("Flee");
        if (packCircle.pack == this)
            packCircle.ClearCircle();
    }
}
