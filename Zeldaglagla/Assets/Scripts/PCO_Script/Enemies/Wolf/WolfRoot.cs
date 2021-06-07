using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monsters;
using Combat;
using Pathfinding;

public class WolfRoot : MonsterRoot 
{
    public float pounceDmg, pounceBuildup, pounceHitSpan, pouceRecover, pounceLength, viewDistance = 20, attackDistance;
    public AttackProfile pounceAtk;
    public PackManager pack;
    public GameObject thisGameObject;
    public bool hasAttacked;
    public PlayerPackCircle packCircle;
    [System.NonSerialized]
    public bool defeated = false;

    public bool isPackLeader;
    // Start is called before the first frame update
    private void Awake()
    {
        thisGameObject = this.gameObject;
        pounceAtk = new AttackProfile(new DamageStruct(pounceDmg), pounceBuildup, pouceRecover, pounceHitSpan);
        pack = GetComponentInParent<PackManager>();
        pather = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindGameObjectWithTag("Player");
        packCircle = player.GetComponent<PlayerPackCircle>();

        AwakeDuPauvre();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void GoToApproach()
    {
        Debug.LogWarning("Moving to Approach"); 
        GetComponent<AIDestinationSetter>().enabled = true;
        if (isPackLeader)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPackCircle>().CreateCircle(pack);
        }
    }

    public override void Death()
    {
        pack.wolves.Remove(this);
        pack.currentWolves--;
        if (pack.currentWolves <= pack.startingWolves / 2)
        {
            pack.AllGoToFlee();
            foreach (WolfRoot wolf in pack.wolves)
            {
                wolf.defeated = true;
            }
        }
        base.Death();
    }


}
